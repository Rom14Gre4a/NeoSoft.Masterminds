using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NeoSoft.Masterminds.Domain;
using NeoSoft.Masterminds.Domain.Interfaces;
using NeoSoft.Masterminds.Domain.Models;
using NeoSoft.Masterminds.Domain.Models.Models;
using NeoSoft.Masterminds.Services.Interfaces;

namespace NeoSoft.Masterminds.Infrastructure.Business
{
    public class MentorService : IMentorService
    {
        private readonly IMentorRepository _mentorRepository;

        public MentorService(IMentorRepository mentorRepository)
        {
            _mentorRepository = mentorRepository;
        }

        public async Task<MentorModel> GetMentorProfileById(int mentorId)
        {
            //var mentor = await _mentorRepository.GetMentor(mentorId);

            return new MentorModel();
            //{
            //    Id = mentor.Id,
            //    FirstName = mentor.Profile.ProfileFirstName,
            //    LastName = mentor.Profile.ProfileLastName,
            //    Description = mentor.Description,
            //    Specialty = mentor.Specialty,
            //    HourlyRate = mentor.HourlyRate,
            //    ProfessionalAspects = mentor.ProfessionalAspects,

            //}
        }
       public async Task<List<MentorListModel>> Get(GetFilter filter)
        {
            var mentorList = await _mentorRepository.Get(filter);
            //foreach (var item in mentorList)
            // {
            //   await СalculateRating(item.Id); // 50
            // }
        }



    }
}
 