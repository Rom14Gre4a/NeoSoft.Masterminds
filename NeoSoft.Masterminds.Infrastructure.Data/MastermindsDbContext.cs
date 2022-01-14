using NeoSoft.Masterminds.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace NeoSoft.Masterminds.Infrastructure.Data
{
    public class MastermindsDbContext : DbContext
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

            //OnAppUserEntityCreating(modelBuilder);


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
            modelBuilder.Entity<MentorEntity>().Property(x => x.HourlyRate);
            modelBuilder.Entity<MentorEntity>()
                .HasOne(me => me.Profile)
                .WithOne()
                .HasForeignKey<MentorEntity>(me => me.Id);
        }
        private void OnReviewEntityCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ReviewEntity>().ToTable("Reviews");
            modelBuilder.Entity<ReviewEntity>().HasKey(p => p.Id);
            modelBuilder.Entity<ReviewEntity>().Property(x => x.Text).IsRequired();
            modelBuilder.Entity<ReviewEntity>()
                 .HasOne(re => re.Owner)
                .WithOne()
                .HasForeignKey<ReviewEntity>(me => me.Id);

            modelBuilder.Entity<ReviewEntity>()
               .HasOne(re => re.Mentor)
               .WithMany(p => p.Reviews)
               .HasForeignKey(p => p.MentorId)
               .OnDelete(DeleteBehavior.NoAction);
        }
        private void OnFileEntityCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FileEntity>().ToTable("Files");
            modelBuilder.Entity<FileEntity>().HasKey(p => p.Id);
            modelBuilder.Entity<FileEntity>().Property(x => x.Name).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<FileEntity>().Property(x => x.InitialName).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<FileEntity>().Property(x => x.ContentType).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<FileEntity>().Property(x => x.Extention).IsRequired().HasMaxLength(100);
        }

    }
}
