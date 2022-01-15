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

        public  async Task SeedFakeData()
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
                ProfileLastName = f.Name.LastName()
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
 //.RuleFor(x => x.ToProfile, f => new ProfileEntity
 //               {
 //                   ProfileFirstName = f.Person.FirstName,
 //                   ProfileLastName = f.Person.LastName,
 //                   //photo
 //               })
    }

}
