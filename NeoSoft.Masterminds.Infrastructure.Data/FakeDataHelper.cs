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
            var mentorFake = new Faker<MentorEntity>()
            .RuleFor(m => m.HourlyRate, f => f.Random.Int(5, 50))
            .RuleFor(m => m.Description, f => f.Lorem.Text())
            .RuleFor(m => m.ProfessionalAspects, f => f.Lorem.Word())
            .RuleFor(m=> m.Specialty, f=> f.Name.JobTitle())
            .RuleFor(m => m.Profile, f => new ProfileEntity
             {
                 ProfileFirstName = f.Name.FirstName(),
                 ProfileLastName = f.Name.LastName()
             });

            _mentors = mentorFake.Generate(mentorCount);

            await _context.AddRangeAsync(_mentors);
        }

    }

}
