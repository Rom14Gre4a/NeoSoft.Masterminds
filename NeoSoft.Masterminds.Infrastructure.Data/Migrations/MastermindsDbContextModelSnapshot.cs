﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NeoSoft.Masterminds.Infrastructure.Data;

namespace NeoSoft.Masterminds.Infrastructure.Data.Migrations
{
    [DbContext(typeof(MastermindsDbContext))]
    partial class MastermindsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.22")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("NeoSoft.Masterminds.Domain.Models.Entities.FileEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ContentType")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Extention")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<int>("FileType")
                        .HasColumnType("int");

                    b.Property<string>("InitialName")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("NeoSoft.Masterminds.Domain.Models.Entities.MentorEntity", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("HourlyRate")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ProfessionalAspects")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Specialty")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Mentors");
                });

            modelBuilder.Entity("NeoSoft.Masterminds.Domain.Models.Entities.ProfileEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("PhotoId")
                        .HasColumnType("int");

                    b.Property<string>("ProfileFirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("ProfileLastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("PhotoId")
                        .IsUnique()
                        .HasFilter("[PhotoId] IS NOT NULL");

                    b.ToTable("Profiles");
                });

            modelBuilder.Entity("NeoSoft.Masterminds.Domain.Models.Entities.ReviewEntity", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int>("MentorId")
                        .HasColumnType("int");

                    b.Property<double>("Rating")
                        .HasColumnType("float");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MentorId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("NeoSoft.Masterminds.Domain.Models.Entities.MentorEntity", b =>
                {
                    b.HasOne("NeoSoft.Masterminds.Domain.Models.Entities.ProfileEntity", "Profile")
                        .WithOne()
                        .HasForeignKey("NeoSoft.Masterminds.Domain.Models.Entities.MentorEntity", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NeoSoft.Masterminds.Domain.Models.Entities.ProfileEntity", b =>
                {
                    b.HasOne("NeoSoft.Masterminds.Domain.Models.Entities.FileEntity", "Photo")
                        .WithOne()
                        .HasForeignKey("NeoSoft.Masterminds.Domain.Models.Entities.ProfileEntity", "PhotoId");
                });

            modelBuilder.Entity("NeoSoft.Masterminds.Domain.Models.Entities.ReviewEntity", b =>
                {
                    b.HasOne("NeoSoft.Masterminds.Domain.Models.Entities.ProfileEntity", "Owner")
                        .WithOne()
                        .HasForeignKey("NeoSoft.Masterminds.Domain.Models.Entities.ReviewEntity", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NeoSoft.Masterminds.Domain.Models.Entities.MentorEntity", "Mentor")
                        .WithMany("Reviews")
                        .HasForeignKey("MentorId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
