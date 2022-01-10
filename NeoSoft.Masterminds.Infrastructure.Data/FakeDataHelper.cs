using Bogus;
using NeoSoft.Masterminds.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NeoSoft.Masterminds.Infrastructure.Data
{
    public class FakeDataHelper
    {
        public static List<MentorEntity> GenerateMentors(int fakeNumber = 20)
        {
            var mentorId = 1;
            var mentorFaker = new Faker<MentorEntity>()
                .CustomInstantiator(f => new MentorEntity { Id = mentorId++ })
                .RuleFor(o => o.Specialty, f => f.)

        }
    }
}
