using Bogus;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NeoSoft.Masterminds.Domain.Models.Entities;
using NeoSoft.Masterminds.Domain.Models.Entities.Identity;
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
        private static List<string> FakeProfessions = new List<string>()
        {
            "Psychologist",
            "Designer",
            "Marketeer",
            "Financier",
            "Economist",
            "Engeneer",
            "Developer",
            "Human Resource manager",
            "software tester",
            "Architect",
            "Analytics",
            "Pharmacist",
            "Producer",
        };
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
        private List<MentorEntity> Mentors { get; set; }
        private readonly MastermindsDbContext _context;

        public FakeDataHelper(MastermindsDbContext contenxt)
        {
            _context = contenxt;
        }

        public async Task SeedFakeData(UserManager<AppUser> userManager)
        {
            var anyMentors = await _context.Mentors.AnyAsync();
            //var a = new Faker<ProfessionEntity>()
            //    .StrictMode(true)
            //    .RuleFor(x => x.Name
                if (!anyMentors)
            {

                var mentors = GenerateMentors(50);
                foreach (var mentor in mentors)
                    await userManager.CreateAsync(mentor);

                var reviewers = GenerateReviewers(50);
                foreach (var reviewer in reviewers)
                    await userManager.CreateAsync(reviewer);


                var reviews = GenerateReviews(mentors.Select(x => x.Id).ToList(), reviewers.Select(x => x.Id).ToList());
                var fakeProfessions = GenerateProfessions();
                await _context.AddRangeAsync(fakeProfessions);



                await _context.Reviews.AddRangeAsync(reviews);
                await _context.SaveChangesAsync();
            }
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

        private List<AppUser> GenerateMentors(List<ProfessionEntity> profi, int fakeNumber = 50)
        {
            var mentors = new List<AppUser>();

            for (int i = 0; i < fakeNumber; i++)
            {
                var faker = new Faker();

                var email = faker.Person.Email;

                mentors.Add(new AppUser
                {
                    UserName = email,
                    Email = email,
                    Profile = new ProfileEntity
                    {
                        ProfileFirstName = faker.Person.FirstName,
                        ProfileLastName = faker.Person.LastName,
                        Photo = GetProfilePhoto(faker.Random.Int(0, ExistingImages.Length - 1)),
                        Mentor = new MentorEntity
                        {
                            Specialty = faker.Person.Company.CatchPhrase,
                            HourlyRate = faker.Random.Decimal(4, 70),
                            ProfessionalAspects = string.Join(", ", faker.Random.WordsArray(1, 4)),
                            Description = faker.Lorem.Paragraph(faker.Random.Int(1, 3)),
                            Professions = GetRandomProfessions() 
                            // new List<ProfessionEntity>
                            //{
                            //    new ProfessionEntity
                            //    {
                            //        Name = 
                            //    }
                            //} //GetRandomProfessions(1, profi), 
                        }
                    },
                }) ;
            }
            return mentors;

        }
        private static List<ProfessionEntity> GenerateProfessions()
        {
            List<ProfessionEntity> professions = new List<ProfessionEntity>();

            foreach(var profession in FakeProfessions)
            {
                professions.Add(new ProfessionEntity
                {
                    Name = profession
                });
            }
            return professions;

        }
        public static List<AppUser> GenerateReviewers(int fakeNumber = 50)
        {
            var reviewers = new List<AppUser>();

            for (int i = 0; i < fakeNumber; i++)
            {
                var faker = new Faker();

                var email = faker.Person.Email;

                reviewers.Add(new AppUser
                {
                    UserName = email,
                    Email = email,
                    Profile = new ProfileEntity
                    {
                        ProfileFirstName = faker.Person.FirstName,
                        ProfileLastName = faker.Person.LastName,
                        Photo = GetProfilePhoto(faker.Random.Int(0, ExistingImages.Length - 1))
                    }
                });
            }

            return reviewers;
        }
        public static List<ReviewEntity> GenerateReviews(List<int> mentorIds, List<int> reviewerIds)
        {
            var reviews = new List<ReviewEntity>();

            var randomizer = new Randomizer();
            var randomMentorIds = mentorIds.Skip(randomizer.Int(0, mentorIds.Count)).Take(randomizer.Int(10, 40)).ToList();

            foreach (var randomMentorId in randomMentorIds)
            {
                var reviewsPart = GenerateReviews(randomMentorId, reviewerIds, randomizer.Int(1, 6));
                reviews.AddRange(reviewsPart);
            }

            return reviews;
        }

        public static List<ReviewEntity> GenerateReviews(int mentorId, List<int> reviewerIds, int fakeNumber = 5)
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
        private static List<ProfessionEntity> GetRandomProfessions(int number, List<ProfessionEntity> profi )
        {
            List<ProfessionEntity> professions = new List<ProfessionEntity>();
            for (int i = 0; i < number; i++)
            {
                var faker = new Faker();
                var profession = profi[faker.Random.Int(0, profi.Count)];
                if (!professions.Contains(profession))
                {
                    professions.Add(profession);
                }
            }
            return professions;
        }
    }
}
