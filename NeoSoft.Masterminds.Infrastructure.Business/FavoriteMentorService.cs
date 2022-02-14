using Microsoft.AspNetCore.Identity;
using NeoSoft.Masterminds.Domain.Interfaces;
using NeoSoft.Masterminds.Domain.Models;
using NeoSoft.Masterminds.Domain.Models.Entities;
using NeoSoft.Masterminds.Domain.Models.Entities.Identity;
using NeoSoft.Masterminds.Domain.Models.Exceptions;
using NeoSoft.Masterminds.Domain.Models.Models;
using NeoSoft.Masterminds.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeoSoft.Masterminds.Infrastructure.Business
{
    public class FavoriteMentorService : IFavoriteMentorService
    {
        private readonly IMentorRepository _mentorRepository;
        private readonly IFavoriteMentorRepository _favoriteMentorRepository;
        private readonly UserManager<AppUser> _userManager;
       
        public FavoriteMentorService(IFavoriteMentorRepository favoriteMentorRepository, UserManager<AppUser> userManager, IMentorRepository mentorRepository)
        {
            _mentorRepository = mentorRepository;
            _favoriteMentorRepository = favoriteMentorRepository;
            _userManager = userManager;
        }
        public async Task<List<MentorListModel>> GetAll(string email)
        {

            var userApp = await _favoriteMentorRepository.GetFavoriteUser(email);
           
            var FavoriteEntity = await _favoriteMentorRepository.GetFavoriteMentorsAsync(userApp.Id);
            if (FavoriteEntity == null)
            {
                throw new NotFoundException($"Profile with this Id => {userApp.Id} was not found");
            }
            var rating = await СalculateRating(FavoriteEntity.Select(m => m.Id).ToArray());
            var list = new List<MentorListModel>();
            foreach (var mentor in FavoriteEntity)
            {
                list.Add(new MentorListModel
                {
                    Id = mentor.Id,
                    FirstName = mentor.Profile.ProfileFirstName,
                    LastName = mentor.Profile.ProfileLastName,
                    ProfilePhotoId = mentor.Profile.PhotoId ?? Constants.UnknownImageId,
                    Rating = rating[mentor.Id],
                    Professions = MyConvertor(mentor.Professions.ToList())

                });
            }
            return list;
        }

        //public async Task<MentorListModel> UpdateFavorites(string email)
        //{
        //    var userApp = await _favoriteMentorRepository.GetFavoriteUser(email);
        //    if (userApp == null)
        //    {

        //    }
        //}
        public async Task<int> FavoritesCount(int mentorId)
        {
            var totalFavorites = await _favoriteMentorRepository.GetProfileTotalFavorites(mentorId);
            return totalFavorites;
        }
       


        private static double СalculateRating(int totalReviews, double ratingSum)
        {
            if (totalReviews == 0 || ratingSum == 0.0)
                return 0.0;

            return Math.Max(Math.Round(ratingSum / totalReviews * 2, MidpointRounding.AwayFromZero) / 2, 0); // 3.2132 => 3.5 // 2.5 // 1.5
        }
       
        private async Task<Dictionary<int, double>> СalculateRating(int[] mentorIds)
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
        private List<ProfessionsModel> MyConvertor(List<ProfessionEntity> source)
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

    }
}
