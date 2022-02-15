using Microsoft.AspNetCore.Identity;
using NeoSoft.Masterminds.Domain.Interfaces;
using NeoSoft.Masterminds.Domain.Models;
using NeoSoft.Masterminds.Domain.Models.Entities.Identity;
using NeoSoft.Masterminds.Domain.Models.Exceptions;
using NeoSoft.Masterminds.Domain.Models.Models;
using NeoSoft.Masterminds.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeoSoft.Masterminds.Infrastructure.Business
{
    public class FavoriteMentorService : IFavoriteMentorService
    {
        private readonly IFavoriteMentorRepository _favoriteMentorRepository;
       
        public FavoriteMentorService(IFavoriteMentorRepository favoriteMentorRepository)
        {
            _favoriteMentorRepository = favoriteMentorRepository;
        }
        public async Task<List<MentorListModel>> GetAll(string email)
        {

            var userApp = await _favoriteMentorRepository.GetFavoriteUser(email);
           
            var FavoriteEntity = await _favoriteMentorRepository.GetFavoriteMentorsAsync(userApp.Id);
            if (FavoriteEntity == null)
            {
                throw new NotFoundException($"Profile with this Id => {userApp.Id} was not found");
            }
            var rating = await Helper.СalculateRating(FavoriteEntity.Select(m => m.Id).ToArray());
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

            if (await _favoriteMentorRepository.GetFavoriteMentorAsync(mentorId) == null)
            {
                await _favoriteMentorRepository.AddFavorite(await _favoriteMentorRepository.GetFavoriteMentorAsync(mentorId), mentorId);
               return true;
            }
            else
            {
                await _favoriteMentorRepository.RemoveFavorite(await _favoriteMentorRepository.GetFavoriteMentorAsync(mentorId), mentorId);
                return true; 
            }
           
        }
        public async Task<int> FavoritesCount(int mentorId)
        {
            var totalFavorites = await _favoriteMentorRepository.GetProfileTotalFavorites(mentorId);
            return totalFavorites;
        }
    }
}
