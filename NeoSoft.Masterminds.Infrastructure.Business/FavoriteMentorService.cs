using Microsoft.AspNetCore.Identity;
using NeoSoft.Masterminds.Domain.Interfaces;
using NeoSoft.Masterminds.Domain.Models;
using NeoSoft.Masterminds.Domain.Models.Entities.Identity;
using NeoSoft.Masterminds.Domain.Models.Exceptions;
using NeoSoft.Masterminds.Domain.Models.Models;
using NeoSoft.Masterminds.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeoSoft.Masterminds.Infrastructure.Business
{
    public class FavoriteMentorService : IFavoriteMentorService
    {
        private readonly IFavoriteMentorRepository _favoriteMentorRepository;
        private readonly IMentorRepository _mentorRepository;

        public FavoriteMentorService(IMentorRepository mentorRepository, IFavoriteMentorRepository favoriteMentorRepository)
        {
            _favoriteMentorRepository = favoriteMentorRepository;
            _mentorRepository = mentorRepository;
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
                    Professions = Helper.MyConvertor(mentor.Professions.ToList())

                });
            }
            return list;
        }
        public async Task<AppUser> GetUserByEmail(string email)
        {
            var profile = await _favoriteMentorRepository.GetFavoriteUser(email);
            return profile;
        }

        public async Task<bool> UpdateFavorites(AppUser user, int mentorId)
        {
            var favorite = await _favoriteMentorRepository.GetFavoriteMentorAsync(user.Id);
            if (favorite != null)
            {
                await _favoriteMentorRepository.AddFavorite(favorite, mentorId);
               return true;
            }
            else
            {
                await _favoriteMentorRepository.RemoveFavorite(favorite, mentorId);
                return true; 
            }
           
        }
        public async Task<int> FavoritesCount(string email)
        {
            
            var totalFavorites = await _favoriteMentorRepository.GetProfileTotalFavorites(email);
            return totalFavorites;
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
