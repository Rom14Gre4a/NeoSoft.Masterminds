using Bogus;
using Microsoft.EntityFrameworkCore;
using NeoSoft.Masterminds.Domain.Models.Entities;
using NeoSoft.Masterminds.Domain.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using NeoSoft.Masterminds.Domain.Models.Entities.Identity;
using Microsoft.AspNetCore.Identity;


namespace NeoSoft.Masterminds.Infrastructure.Data
{
    public static class FakeDataHelper 

    {

        public static async Task SeedFakeData(MastermindsDbContext context, UserManager<AppUser> userManager)
        {
            var anyMentors = await context.Mentors.AnyAsync();
            if (!anyMentors)
            {
                var mentors = GenerateMentors(50);
                foreach (var mentor in mentors)
                    await userManager.CreateAsync(mentor, "Asd123!1");
                //await context.Mentors.AddRangeAsync(mentors);
                await context.SaveChangesAsync();
            }
           
        }
        public static List<AppUser> GenerateMentors(int fakeNumber = 50)
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
                            HourlyRate = faker.Random.Decimal(4, 20),
                            ProfessionalAspects = String.Join(", ", faker.Random.WordsArray(1, 4)),
                            Description = faker.Lorem.Paragraphs(faker.Random.Int(1, 3)),
                        }
                    }
                });
            }
                //.CustomInstantiator(f => new MentorEntity { Id = mentorId++ })
                //.RuleFor(o => o.Specialty, f => f.Person.)//Enum<SpecialtyType>())
                //.RuleFor(o => o.HourlyRate, f => f.Random.Decimal(10, 30))
                //.RuleFor(o => o.Description, f => f.Lorem.Paragraph(f.Random.Int(1, 3)))
                //.RuleFor(o => o.ProfessionalAspects, f => string.Join(", ", f.Random.WordsArray(1, 4))); // Random.WordsArray(1, 3))
               //.RuleFor(o => o.Profile, f => new ProfileEntity
               // {
               //     ProfileFirstName = f.Person.FirstName,
               //     ProfileLastName = f.Person.LastName,
               //    Photo = GetProfilePhoto(f.Random.Int(0, ExistingImages.Length - 1))
               // })
               // .RuleFor(o => o.Reviews, f => reviews.Skip(f.Random.Int(0, fakeNumber - 20)).Take(f.Random.Int(0, 20)).ToList());

            //var mentors = mentors.Generate(fakeNumber);
            return mentors;

        }

        private static readonly string[] ExistingImages = new[]
        {
            "srsnsn"
        };
        private static FileEntity GetProfilePhoto(int index)
        {
            return new FileEntity
            {

            };
        }
    }

}
