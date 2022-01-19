using Bogus;
using Microsoft.EntityFrameworkCore;
using NeoSoft.Masterminds.Domain.Models.Entities;
using NeoSoft.Masterminds.Domain.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NeoSoft.Masterminds.Infrastructure.Data
{
    public class FakeDataHelper

    {
        private List<MentorEntity> _mentors { get; set; }
        private readonly MastermindsDbContext _context;

        public FakeDataHelper(MastermindsDbContext contenxt)
        {
            _context = contenxt;
        }

        public async Task SeedFakeData()
        {
            var anyMentors = await _context.Mentors.AnyAsync();

            if (!anyMentors)
            {
                await CreateMentorsAndProfilesAsync(100);
                await _context.SaveChangesAsync();
            }
        }

        private async Task CreateMentorsAndProfilesAsync(int mentorCount)
        {
            var reviews = GenerateReviews();

            var mentorFaker = new Faker<MentorEntity>()
            .RuleFor(m => m.HourlyRate, f => f.Random.Int(5, 50))
            .RuleFor(m => m.Description, f => f.Lorem.Text())
            .RuleFor(m => m.ProfessionalAspects, f => f.Lorem.Word())
            .RuleFor(m => m.Specialty, f => f.Name.JobTitle())
            .RuleFor(m => m.Profile, f => new ProfileEntity
            {
                ProfileFirstName = f.Name.FirstName(),
                ProfileLastName = f.Name.LastName(),
                Photo = GetProfilePhoto(f.Random.Int(0, ExistingImages.Length - 1))
            })
            .RuleFor(m => m.Reviews, f => reviews.ToList());


            _mentors = mentorFaker.Generate(mentorCount);

            await _context.AddRangeAsync(_mentors);
        }
        private List<ReviewEntity> GenerateReviews(int fakeNumber = 50)
        {
            var reviewFaker = new Faker<ReviewEntity>()
                .RuleFor(x => x.FromProfile, f => new ProfileEntity
                {
                    ProfileFirstName = f.Person.FirstName,
                    ProfileLastName = f.Person.LastName,
                })
                .RuleFor(x => x.ToProfile, f => new ProfileEntity
                {
                    ProfileFirstName = f.Person.FirstName,
                    ProfileLastName = f.Person.LastName,
                })
                .RuleFor(x => x.Rating, f => f.Random.Double(1, 5))
                .RuleFor(x => x.Text, f => f.Random.Words());


            return reviewFaker.Generate(fakeNumber);
        }
        private static FileEntity GetProfilePhoto(int index)
        {
            if (index > 7)
                return null;

            return new FileEntity
            {
                InitialName = $"ProfilePhoto_{index}",
                Name = ExistingImages[index],
                ContentType = index == 4 ? "image/jpeg" : "image/png",
                Extension = index == 4 ? "jpg" : "png",
                FileType = FileType.ProfilePhoto
            };

        }
        private static readonly string[] ExistingImages = new[]
        {
            "53387f51-b2dc-4dea-8b79-74273a8145ce",
            "12b0ef8e-59bd-4334-8aa6-01af5dc065fd",
            "3d19de4c-20c0-418f-9329-0c03365a2f77",      
            "5fd7e89c-319d-492b-848c-f5ca89f114a1",
            "9387a87f-9c25-4467-aa8b-ba470976fde5",
            "b345f041-d817-44b3-b5cb-4605be6f1987",
            "c289844f-a838-4c9d-a03f-5354f0a6b070",
            "e8b56e48-ac8b-4113-9372-18e5598ba662",
        };

    }
}
