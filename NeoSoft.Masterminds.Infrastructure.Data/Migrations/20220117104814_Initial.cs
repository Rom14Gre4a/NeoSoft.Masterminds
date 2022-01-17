﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace NeoSoft.Masterminds.Infrastructure.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    InitialName = table.Column<string>(maxLength: 100, nullable: false),
                    ContentType = table.Column<string>(maxLength: 100, nullable: false),
                    Extention = table.Column<string>(maxLength: 100, nullable: false),
                    FileType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Profiles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhotoId = table.Column<int>(nullable: true),
                    MentorId = table.Column<int>(nullable: true),
                    ProfileFirstName = table.Column<string>(maxLength: 50, nullable: false),
                    ProfileLastName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Profiles_Files_PhotoId",
                        column: x => x.PhotoId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Mentors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Specialty = table.Column<string>(maxLength: 50, nullable: false),
                    HourlyRate = table.Column<decimal>(type: "Decimal(4,2)", nullable: false),
                    ProfessionalAspects = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mentors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mentors_Profiles_Id",
                        column: x => x.Id,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FromProfileId = table.Column<int>(nullable: false),
                    ToProfileId = table.Column<int>(nullable: false),
                    Text = table.Column<string>(nullable: false),
                    Rating = table.Column<double>(nullable: false),
                    MentorEntityId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_Profiles_FromProfileId",
                        column: x => x.FromProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reviews_Mentors_MentorEntityId",
                        column: x => x.MentorEntityId,
                        principalTable: "Mentors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reviews_Profiles_ToProfileId",
                        column: x => x.ToProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_MentorId",
                table: "Profiles",
                column: "MentorId");

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_PhotoId",
                table: "Profiles",
                column: "PhotoId",
                unique: true,
                filter: "[PhotoId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_FromProfileId",
                table: "Reviews",
                column: "FromProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_MentorEntityId",
                table: "Reviews",
                column: "MentorEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ToProfileId",
                table: "Reviews",
                column: "ToProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Profiles_Mentors_MentorId",
                table: "Profiles",
                column: "MentorId",
                principalTable: "Mentors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mentors_Profiles_Id",
                table: "Mentors");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Profiles");

            migrationBuilder.DropTable(
                name: "Mentors");

            migrationBuilder.DropTable(
                name: "Files");
        }
    }
}