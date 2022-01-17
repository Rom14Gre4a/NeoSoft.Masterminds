using System;
using System.Collections.Generic;
using System.Linq;
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
            var mentorEntity = await _mentorRepository.GetMentorProfileById(mentorId);
            if (mentorEntity == null)
                return null;

            var mentor = new MentorModel
            {
                Id = mentorEntity.Id,
                FirstName = mentorEntity.Profile.ProfileFirstName,
                LastName = mentorEntity.Profile.ProfileLastName,
                Description = mentorEntity.Description,
                Specialty = mentorEntity.Specialty,
                HourlyRate = mentorEntity.HourlyRate,
                ProfessionalAspects = mentorEntity.ProfessionalAspects.Split(", ").ToList(),
                //Reviews = mentorEntity.Reviews.Select(x => new ReviewModel
                //{
                //    Id = x.Id,
                //    FirstName = x.SentReviews.FirstName,
                //    LastName = x.SentReviews.LastName,
                //    ProfilePhotoId = x.Owner.PhotoId ?? Constants.UnknownImageId,
                //    Text = x.Text,
                //    Rating = x.Rating
                //}).ToList(),
            };

            return new MentorModel();
            
        }
        public async Task<List<MentorListModel>> Get(GetFilter filter)
        {
            var mentorListDb = await _mentorRepository.Get(filter);
           var list = new List<MentorListModel>();
            foreach (var mentor in mentorListDb)
            {
                list.Add(new MentorListModel
                {
                    Id = mentor.Id,
                    FirstName = mentor.Profile.ProfileFirstName,
                    LastName = mentor.Profile.ProfileLastName,
                    Specialty = mentor.Specialty,
                    HourlyRate = mentor.HourlyRate,
                    Description = mentor.Description,
                });
            }

            return list;
        }



    }
}
 