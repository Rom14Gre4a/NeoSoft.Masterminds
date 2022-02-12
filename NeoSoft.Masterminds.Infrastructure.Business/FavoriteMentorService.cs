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
using System.Text;
using System.Threading.Tasks;

namespace NeoSoft.Masterminds.Infrastructure.Business
{
    public class FavoriteMentorService : IFavoriteMentorService
    {
        private readonly IFavoriteMentorRepository _favoriteMentorRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IProfileRepository _profileRepository;
        public FavoriteMentorService(IFavoriteMentorRepository favoriteMentorRepository, UserManager<AppUser> userManager, IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
            _favoriteMentorRepository = favoriteMentorRepository;
            _userManager = userManager;
        }
        public async Task<List<MentorListModel>> GetAll(string email)
        {
            var userApp = await _userManager.FindByEmailAsync(email);
            if (userApp == null)
            {
                throw new NotFoundException($"AppUser with this email => {email} was not found");
            }
            var profileEntity = await _profileRepository.GetProfileById(userApp.Id);
            if (profileEntity == null)
            {
                throw new NotFoundException($"Profile with this Id => {userApp.Id} was not found");
            }



            //var userProfiles = profileEntity.Favorites.Where(MentorId => Favorites.Contains(Id)).ToList();
            var a = profileEntity.Favorites.ToList();
            var favoriteListDb = await _favoriteMentorRepository.GetAll();
            var list = new List<MentorListModel>();

            //var rating = await СalculateRating(favoriteListDb.Select(m => m.Id).ToArray());
            foreach (var mentor in favoriteListDb)
            {
                list.Add(new MentorListModel
                {
                    Id = mentor.Id,
                    FirstName = mentor.Profile.ProfileFirstName,
                    LastName = mentor.Profile.ProfileLastName,
                    ProfilePhotoId = mentor.Profile.PhotoId ?? Constants.UnknownImageId,
                    //Rating = rating[mentor.Id],
                    //Professions = MyConvertor(mentor.Professions.ToList())

                });
            }


            //var selectedUsers = from user in list
            //                    where user.Id == userApp.Id
            //                    select user;
            return list;
        }
        
    }
}
