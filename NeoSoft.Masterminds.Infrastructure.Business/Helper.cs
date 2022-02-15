using NeoSoft.Masterminds.Domain.Interfaces;
using NeoSoft.Masterminds.Domain.Models.Entities;
using NeoSoft.Masterminds.Domain.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NeoSoft.Masterminds.Infrastructure.Business
{
    public static class Helper
    {
        private static readonly IMentorRepository _mentorRepository;
        public static List<ProfessionsModel> MyConvertor(List<ProfessionEntity> source)
        {
            List<ProfessionsModel> dest = new List<ProfessionsModel>();
            foreach (var sourceItem in source)
            {
                dest.Add(new ProfessionsModel
                {
                    Id = sourceItem.Id,
                    Name = sourceItem.Name
                });
            }
            return dest;
        }

        public static async Task<Dictionary<int, double>> СalculateRating(int[] mentorIds)
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
        public static async Task<double> СalculateRating(int mentorId)
        {
            var totalReviews = await _mentorRepository.GetMentorTotalReviews(mentorId);
            var ratingSum = await _mentorRepository.GetMentorRatingSum(mentorId);

            return СalculateRating(totalReviews, ratingSum);
        }
    }
}
