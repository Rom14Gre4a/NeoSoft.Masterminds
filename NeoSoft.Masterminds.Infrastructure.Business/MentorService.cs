using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NeoSoft.Masterminds.Domain;
using NeoSoft.Masterminds.Domain.Interfaces;
using NeoSoft.Masterminds.Domain.Models;
using NeoSoft.Masterminds.Domain.Models.Entities;
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
            var mentor = await _mentorRepository.GetMentorProfileById(mentorId);
            if (mentor == null)
                return null;

            var rating = await СalculateRating(mentorId);
            
            var mentorModel = ConvertEntityModelToMentorModel(mentor, rating);
            return mentorModel;

        }
        private MentorModel ConvertEntityModelToMentorModel(MentorEntity mentor, double rating)
        {
            return new MentorModel
            {
                Id = mentor.Id,
                FirstName = mentor.Profile.ProfileFirstName,
                LastName = mentor.Profile.ProfileLastName,
                Description = mentor.Description,
                Specialty = mentor.Specialty,
                Rating = mentor.Rating,
                HourlyRate = mentor.HourlyRate,
                ProfessionalAspects = mentor.ProfessionalAspects.Select(x => x.Aspect).ToList(),
                Reviews = mentor.Profile.RecivedReviews.Select(x => new ReviewModel
                {
                    Id = x.Id,
                    FirstName = x.ToProfile.ProfileFirstName,
                    LastName = x.ToProfile.ProfileLastName,
                    ProfilePhotoId = x.ToProfile.PhotoId ?? Constants.UnknownImageId,
                    Text = x.Text,
                    
                }).ToList(),
            };
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

        private static double СalculateRating(int totalReviews, double ratingSum)
        {
            if (totalReviews == 0 || ratingSum == 0.0)
                return 0.0;

            return Math.Max(Math.Round(ratingSum / totalReviews * 2, MidpointRounding.AwayFromZero) / 2, 0); // 3.2132 => 3.5 // 2.5 // 1.5
        }
        private async Task<double> СalculateRating(int mentorId)
        {
            var totalReviews = await _mentorRepository.GetMentorTotalReviews(mentorId);
            var ratingSum = await _mentorRepository.GetMentorRatingSum(mentorId);

            return СalculateRating(totalReviews, ratingSum);
        }

    }
}
 