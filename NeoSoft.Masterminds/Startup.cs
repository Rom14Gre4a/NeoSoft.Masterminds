using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using NeoSoft.Masterminds.Infrastructure.Data;
using NeoSoft.Masterminds.Infrastructure.Business;
using NeoSoft.Masterminds.Services.Interfaces;
using NeoSoft.Masterminds.Domain.Interfaces;
using NeoSoft.Masterminds.Infrastructure.Data.Repositories;
using System.Text.Json.Serialization;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using NeoSoft.Masterminds.Domain.Models.Entities.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using NeoSoft.Masterminds.Domain.Models.Options;
using NeoSoft.Masterminds.Middleware;
using System.Reflection;
using NeoSoft.Masterminds.Infrastructure.Business.MapServices;

namespace NeoSoft.Masterminds
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

       
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins("http://localhost:4200", "https://localhost:4200").AllowAnyHeader().AllowAnyMethod().AllowCredentials();
                });
            });
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.Configure<JwtTokenOptions>(Configuration.GetSection(nameof(JwtTokenOptions)));
            services.AddDbContext<MastermindsDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            
            services.AddScoped<IMentorRepository, MentorRepository>();
            services.AddScoped<IMentorService, MentorService>();

            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IFileRepository, FileRepository>();

            services.AddScoped<IProfessionRepository, ProfessionRepository>();
            services.AddScoped<IProfessionService, ProfessionService>();

            services.AddScoped<IProfessionalAspectRepository, ProfessionalAspectRepository>();
            services.AddScoped<IProfessionAspectService, ProfessionAspectService>();

            services.AddScoped<IJwtTokenService, JwtTokenService>();
            services.AddScoped<IAccountService, AccountService>();

            services.AddScoped<IUserProfileService, UserProfileService>();
            services.AddScoped<IProfileRepository, ProfileRepository>();

            services.AddScoped<IFavoriteMentorRepository, FavoriteMentorRepository>();
            services.AddScoped<IFavoriteMentorService, FavoriteMentorService>();


            services.AddIdentity<AppUser, AppRole>(options =>{
                options.User.RequireUniqueEmail = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;  
            })

               .AddEntityFrameworkStores<MastermindsDbContext>();
           
            services.AddControllers()
                 .AddJsonOptions(options =>
                 {
                     options.JsonSerializerOptions.IgnoreNullValues = true;
                     options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                     options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                 });
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            })
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidIssuer = Configuration["JwtTokenOptions:Issuer"],
                            ValidateAudience = true,
                            ValidAudience = Configuration["JwtTokenOptions:Audience"],
                            ValidateLifetime = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["JwtTokenOptions:SecurityKey"])),
                            ValidateIssuerSigningKey = true,
                        };
                    }); 
            services.AddFluentValidation(options =>
             {
                 options.RegisterValidatorsFromAssemblyContaining<Startup>();
             });
            services.AddAutoMapper(Assembly.GetExecutingAssembly(), typeof(ProfileEntityMap).Assembly);
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "NeoSoft.Masterminds", Version = "v1" });
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Scheme = "Bearer",
                    Type = SecuritySchemeType.ApiKey,
                    Description = "Please insert JWT with Bearer into field"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer",
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        }, new string[] { }
                    }
                });
            });
            

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, MastermindsDbContext context, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager) 
        {
            var databaseMigrateTask = Task.Run(() => context.Database.MigrateAsync());
            databaseMigrateTask.Wait();

            if (env.IsDevelopment() || env.IsProduction())
            {
                var seedFakeDataTask = Task.Run(() => new FakeDataHelper(context, userManager, roleManager).SeedFakeData());
                seedFakeDataTask.Wait();

                
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("v1/swagger.json", "Masterminds Api V1");
                });
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
