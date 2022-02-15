using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using NeoSoft.Masterminds.Domain;
using NeoSoft.Masterminds.Domain.Interfaces;
using NeoSoft.Masterminds.Domain.Models;
using NeoSoft.Masterminds.Domain.Models.Exceptions;
using NeoSoft.Masterminds.Domain.Models.Filters;
using NeoSoft.Masterminds.Domain.Models.Models;
using NeoSoft.Masterminds.Domain.Models.Responses;
using NeoSoft.Masterminds.Services.Interfaces;

namespace NeoSoft.Masterminds.Infrastructure.Business
{
    public class MentorService : IMentorService
    {
        private readonly IMentorRepository _mentorRepository;
        private readonly IMapper _mapper;
        public MentorService(IMentorRepository mentorRepository, IMapper mapper )         
        {
            _mentorRepository = mentorRepository;
            _mapper = mapper;
        }

        public async Task<MentorModel> GetMentorProfileById(int mentorId)
        {
            
            var mentor = await _mentorRepository.GetMentorProfileById(mentorId);
            var listMentor = await _mentorRepository.GetAllMentorProfiles();
            if (mentorId > listMentor.Count - 1)
                throw new ValidationErrorException(new ValidationMessage
                {
                    Field = "Id",
                    Messages = new List<string> {$"Mentor with {mentorId} not found in the system" }
                });

            var rating = await СalculateRating(mentorId);


            return new MentorModel
            {
                Id = mentor.Id,
                FirstName = mentor.Profile.ProfileFirstName,
                LastName = mentor.Profile.ProfileLastName,
                Description = mentor.Description,
                HourlyRate = mentor.HourlyRate,
                Rating = rating,
                ProfessionalAspects = mentor.ProfessionalAspects.Select(x => x.Aspect).ToList(),
                Professions = mentor.Professions.Select(x => x.Name).ToList(),
                ReviewsTotalCount = mentor.Profile.RecivedReviews.Count(),
                Reviews = mentor.Profile.RecivedReviews.Select(x => new ReviewModel
                {
                    Id = x.Id,
                    FirstName = x.ToProfile.ProfileFirstName,
                    LastName = x.ToProfile.ProfileLastName,
                    ProfilePhotoId = x.ToProfile.PhotoId ?? Constants.UnknownImageId,
                    Text = x.Text,
                    Rating = x.Rating,
                    ReviewDate = x.ReviewDate,

                }).ToList()
            };
        }

        public async Task<List<MentorListModel>> Get(MentorSearchFilter filter)
        {
            var mentorListDb = await _mentorRepository.Get(filter);
           var list = new List<MentorListModel>();
            var rating = await СalculateRating(mentorListDb.Select(m => m.Id).ToArray());
            foreach (var mentor in mentorListDb)
            {
                list.Add(new MentorListModel
                {
                    Id = mentor.Id,
                    FirstName = mentor.Profile.ProfileFirstName,
                    LastName = mentor.Profile.ProfileLastName,
                    ProfilePhotoId = mentor.Profile.PhotoId ?? Constants.UnknownImageId,
                    Rating = rating[mentor.Id],
                    Professions = Helper.MyConvertor(mentor.Professions.ToList())
                });
            }
            return list;
        }
        public async Task<Dictionary<int, double>> СalculateRating(int[] mentorIds)
        {
            var totalReviews = await _mentorRepository.GetMentorTotalReviews(mentorIds);
            var ratingSums = await _mentorRepository.GetMentorRatingSum(mentorIds);

            var result = new Dictionary<int, double>();

            foreach (var mentorId in mentorIds)
            {
                var mentorRating = 0.0;

                if (totalReviews.ContainsKey(mentorId) && ratingSums.ContainsKey(mentorId))
                {
                    var ratingSum = ratingSums[mentorId];
                    var totalReview = totalReviews[mentorId];

                    mentorRating = СalculateRating(totalReview, ratingSum);
                }

                result.Add(mentorId, mentorRating);
            }

            return result;
        }
        private static double СalculateRating(int totalReviews, double ratingSum)
        {
            if (totalReviews == 0 || ratingSum == 0.0)
                return 0.0;

            return Math.Max(Math.Round(ratingSum / totalReviews * 2, MidpointRounding.AwayFromZero) / 2, 0);
        }
        public  async Task<double> СalculateRating(int mentorId)
        {
            var totalReviews = await _mentorRepository.GetMentorTotalReviews(mentorId);
            var ratingSum = await _mentorRepository.GetMentorRatingSum(mentorId);

            return СalculateRating(totalReviews, ratingSum);
        }
    }
}
 