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

namespace NeoSoft.Masterminds
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MastermindsDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IMentorRepository, MentorRepository>();
            services.AddScoped<IMentorService, MentorService>();

            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IFileRepository, FileRepository>();

            services.AddControllers()
                 .AddJsonOptions(options =>
                 {
                     options.JsonSerializerOptions.IgnoreNullValues = true;
                     options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                     options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                 })
            .AddFluentValidation(options =>
             {
                 options.RegisterValidatorsFromAssemblyContaining<Startup>();
             });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "NeoSoft.Masterminds", Version = "v1" });
            });
            

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, MastermindsDbContext context) 
        {
            var databaseMigrateTask = Task.Run(() => context.Database.MigrateAsync());
            databaseMigrateTask.Wait();

            if (env.IsDevelopment())
            {
                var seedFakeDataTask = Task.Run(() => new FakeDataHelper(context).SeedFakeData());
                seedFakeDataTask.Wait();

                
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("v1/swagger.json", "NeoSoft.Masterminds V1");
                });
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
 
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
