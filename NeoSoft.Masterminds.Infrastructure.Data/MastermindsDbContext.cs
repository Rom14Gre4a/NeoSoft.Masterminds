using NeoSoft.Masterminds.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using NeoSoft.Masterminds.Domain.Models.Enums;
using NeoSoft.Masterminds.Domain.Models;
using NeoSoft.Masterminds.Domain.Models.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Collections.Generic;

namespace NeoSoft.Masterminds.Infrastructure.Data
{
    public class MastermindsDbContext : IdentityDbContext<AppUser, AppRole, int, AppIdentityUserClaim, AppIdentityUserRole, AppIdentityUserLogin, AppIdentityRoleClaim, AppIdentityUserToken>
    {
        public MastermindsDbContext(DbContextOptions<MastermindsDbContext> options)
           : base(options)
        {
        }
        public DbSet<ProfileEntity> Profiles { get; set; }
        public DbSet<MentorEntity> Mentors { get; set; }
        public DbSet<ProfessionEntity> Professions { get; set; }
        public DbSet<ProfessionalAspectEntity> ProfessionalAspects { get; set; }
        public DbSet<ReviewEntity> Reviews { get; set; }
        public DbSet<FileEntity> Files { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            OnProfileEntityCreating(modelBuilder);
            OnMentorEntityCreating(modelBuilder);
            OnReviewEntityCreating(modelBuilder);
            OnFileEntityCreating(modelBuilder);

          
            OnProfessionTableCreating(modelBuilder);
            OnProfessionalAspectsTableCreating(modelBuilder);

            OnAppUserEntityCreating(modelBuilder);
            OnAppRoleEntityCreating(modelBuilder);
            OnAppIdentityUserClaimCreating(modelBuilder);
            OnAppIdentityUserRoleCreating(modelBuilder);
            OnAppIdentityUserLoginCreating(modelBuilder);
            OnAppIdentityRoleClaimCreating(modelBuilder);
            OnAppIdentityUserTokenCreating(modelBuilder);
        }

        private void OnProfileEntityCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProfileEntity>().ToTable("Profiles");
            modelBuilder.Entity<ProfileEntity>().HasKey(p => p.Id);
            modelBuilder.Entity<ProfileEntity>().Property(x => x.ProfileFirstName).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<ProfileEntity>().Property(x => x.ProfileLastName).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<ProfileEntity>()
                .HasOne(pe => pe.Photo)
                .WithOne()
                .HasForeignKey<ProfileEntity>(pe => pe.PhotoId);
        }
       
        private void OnMentorEntityCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MentorEntity>().ToTable("Mentors");
            modelBuilder.Entity<MentorEntity>().HasKey(p => p.Id);
            modelBuilder.Entity<MentorEntity>().Property(x => x.Description).IsRequired();
            modelBuilder.Entity<MentorEntity>().Property(x => x.HourlyRate).HasColumnType("decimal(8,2)");
            modelBuilder.Entity<MentorEntity>()
                .HasOne(me => me.Profile)
                .WithOne(x => x.Mentor)
                .HasForeignKey<MentorEntity>(me => me.Id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MentorEntity>()
               .HasMany(s => s.fans)
               .WithMany(s => s.Favorites)
               .UsingEntity<Dictionary<string, object>>("Favorites",
               p => p.HasOne<ProfileEntity>().WithMany().OnDelete(DeleteBehavior.NoAction),
               p => p.HasOne<MentorEntity>().WithMany().OnDelete(DeleteBehavior.NoAction));

            modelBuilder.Entity<MentorEntity>()
                .HasMany(s => s.Professions)
                .WithMany(s => s.Mentors)
                .UsingEntity(j => j.ToTable("MentorsProfessions"));
            
            modelBuilder.Entity<MentorEntity>()
               .HasMany(s => s.ProfessionalAspects)
               .WithMany(s => s.Mentors)
               .UsingEntity(j => j.ToTable("MentorsProfessionalAspects"));     
        }
       
        private void OnReviewEntityCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ReviewEntity>().ToTable("Reviews");
            modelBuilder.Entity<ReviewEntity>().HasKey(p => p.Id);
            modelBuilder.Entity<ReviewEntity>().Property(x => x.Text).IsRequired();
            modelBuilder.Entity<ReviewEntity>().Property(x => x.Rating).IsRequired();
            modelBuilder
                .Entity<ReviewEntity>()
                .HasOne(r => r.FromProfile)         
                .WithMany(r => r.SentReviews)        
                .HasForeignKey(r => r.FromProfileId)  
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder
                .Entity<ReviewEntity>()
               .HasOne(r => r.ToProfile)             
               .WithMany(p => p.RecivedReviews)      
               .HasForeignKey(r => r.ToProfileId)   
               .OnDelete(DeleteBehavior.NoAction);
        }
        private void OnFileEntityCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FileEntity>().ToTable("Files");
            modelBuilder.Entity<FileEntity>().HasKey(p => p.Id);
            modelBuilder.Entity<FileEntity>().Property(x => x.Name).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<FileEntity>().Property(x => x.InitialName).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<FileEntity>().Property(x => x.ContentType).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<FileEntity>().Property(x => x.Extension).IsRequired().HasMaxLength(100);

            modelBuilder.Entity<FileEntity>().HasData(new FileEntity
            {
                Id = Constants.UnknownImageId,
                Name = Constants.UnknownImageName,
                InitialName = "Unknown",
                FileType = FileType.ProfilePhoto,
                Extension = "jpg",
                ContentType = "image/jpeg"
            });
        }
        private void OnProfessionTableCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProfessionEntity>().ToTable("Professions");
            modelBuilder.Entity<ProfessionEntity>().HasKey(p => p.Id);
            modelBuilder.Entity<ProfessionEntity>().Property(p => p.Name)
                .IsRequired().HasMaxLength(100);
        }

        private void OnProfessionalAspectsTableCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProfessionalAspectEntity>().ToTable("ProfessionalAspects");
            modelBuilder.Entity<ProfessionalAspectEntity>().HasKey(p => p.Id);
            modelBuilder.Entity<ProfessionalAspectEntity>().Property(p => p.Aspect)
                .IsRequired().HasMaxLength(100);
        }

        private void OnAppUserEntityCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppUser>().ToTable("AppUsers");

            modelBuilder.Entity<AppUser>()
                .HasOne(appUser => appUser.Profile)
                .WithOne(p => p.AppUser)
                .HasForeignKey<AppUser>(appUser => appUser.Id);
        }

        private void OnAppRoleEntityCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppRole>().ToTable("AppRoles");
            modelBuilder.Entity<AppRole>()
                .HasData(new AppRole
                {
                    Id = 1,
                    Name = "User",
                    NormalizedName = "USER",
                });

            modelBuilder.Entity<AppRole>()
                .HasData(new AppRole
                {
                    Id = 2,
                    Name = "Mentor",
                    NormalizedName = "MENTOR",
                });
        }


        private void OnAppIdentityUserClaimCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppIdentityUserClaim>().ToTable("AppIdentityUserClaims");
        }

        private void OnAppIdentityUserRoleCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppIdentityUserRole>().ToTable("AppIdentityUserRoles");
        }

        private void OnAppIdentityUserLoginCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppIdentityUserLogin>().ToTable("AppIdentityUserLogins");
        }

        private void OnAppIdentityRoleClaimCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppIdentityRoleClaim>().ToTable("AppIdentityRoleClaims");
        }

        private void OnAppIdentityUserTokenCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppIdentityUserToken>().ToTable("AppIdentityUserTokens");
        }


    }
}
