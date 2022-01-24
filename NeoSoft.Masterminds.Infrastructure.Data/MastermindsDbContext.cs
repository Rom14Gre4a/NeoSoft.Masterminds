using NeoSoft.Masterminds.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using NeoSoft.Masterminds.Domain.Models.Enums;
using NeoSoft.Masterminds.Domain.Models;
using NeoSoft.Masterminds.Domain.Models.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

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
        public DbSet<ReviewEntity> Reviews { get; set; }
        public DbSet<FileEntity> Files { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            OnProfileEntityCreating(modelBuilder);
            OnMentorEntityCreating(modelBuilder);
            OnReviewEntityCreating(modelBuilder);
            OnFileEntityCreating(modelBuilder);

            OnAppUserEntityCreating(modelBuilder);
            OnAppRoleEntityCreating(modelBuilder);
            OnAppIdentityUserClaimCreating(modelBuilder);
            OnAppIdentityUserRoleCreating(modelBuilder);
            OnAppIdentityUserLoginCreating(modelBuilder);
            OnAppIdentityRoleClaimCreating(modelBuilder);
            OnAppIdentityUserTokenCreating(modelBuilder);
            OnProfessionEntityCreating(modelBuilder);
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
            modelBuilder.Entity<MentorEntity>().Property(x => x.Specialty).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<MentorEntity>().Property(x => x.ProfessionalAspects).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<MentorEntity>().Property(x => x.Description).IsRequired();
            modelBuilder.Entity<MentorEntity>().Property(x => x.HourlyRate).HasColumnType("Decimal(4,2)");
            modelBuilder.Entity<MentorEntity>()
                .HasOne(me => me.Profile)
                .WithOne(x => x.Mentor)
                .HasForeignKey<MentorEntity>(me => me.Id);
          
        }
        private void OnProfessionEntityCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProfessionEntity>().ToTable("Professions");
            modelBuilder.Entity<ProfessionEntity>().HasKey(p => p.Id);
            modelBuilder.Entity<ProfessionEntity>().Property(x => x.Name).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<ProfessionEntity>()
                .HasMany(x => x.Name)
                .WithMany(x => x.Mentors)
                .HasForeignKey<ProfessionEntity>(me => me.Id);
        }
        private void OnReviewEntityCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ReviewEntity>().ToTable("Reviews");
            modelBuilder.Entity<ReviewEntity>().HasKey(p => p.Id);
            modelBuilder.Entity<ReviewEntity>().Property(x => x.Text).IsRequired();
            modelBuilder
                .Entity<ReviewEntity>()
                .HasOne(r => r.FromProfile)         //від кого
                .WithMany(r => r.SentReviews)        // отримав    
                .HasForeignKey(r => r.FromProfileId) // від кого айді   
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder
                .Entity<ReviewEntity>()
               .HasOne(r => r.ToProfile)             //Кому 
               .WithMany(p => p.RecivedReviews)      //відправив
               .HasForeignKey(r => r.ToProfileId)    //кому відправив айді
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
