using Bogus;
using Microsoft.EntityFrameworkCore;
using NeoSoft.Masterminds.Domain.Models.Entities;
using NeoSoft.Masterminds.Domain.Models.Enums;
using System;
using System.Collections.Generic;
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
                await GenerateReviews(100);
                await _context.SaveChangesAsync();
            }
        }

        private async Task CreateMentorsAndProfilesAsync(int mentorCount)
        {
            var mentorFaker = new Faker<MentorEntity>()
            .RuleFor(m => m.HourlyRate, f => f.Random.Int(5, 50))
            .RuleFor(m => m.Description, f => f.Lorem.Text())
            .RuleFor(m => m.ProfessionalAspects, f => f.Lorem.Word())
            .RuleFor(m=> m.Specialty, f=> f.Name.JobTitle())
            .RuleFor(m => m.Profile, f => new ProfileEntity
             {
                 ProfileFirstName = f.Name.FirstName(),
                 ProfileLastName = f.Name.LastName()
             });

            _mentors = mentorFaker.Generate(mentorCount);

            await _context.AddRangeAsync(_mentors);
        }
        private async Task GenerateReviews(int fakeNumber)
        {
            var reviewId = 1;
            var reviewFaker = new Faker<ReviewEntity>()
                 .CustomInstantiator(f => new ReviewEntity { Id = reviewId++ })
                .RuleFor(x => x.Owner, f => new ProfileEntity
                {
                    ProfileFirstName = f.Person.FirstName,
                    ProfileLastName = f.Person.LastName,
                    //photo
                })
                .RuleFor(x => x.Rating, f => f.Random.Double(1, 5))
                .RuleFor(x => x.Text, f => f.Random.Words());
             

            await _context.AddRangeAsync(reviewFaker.Generate(fakeNumber));
        }

    }

}
