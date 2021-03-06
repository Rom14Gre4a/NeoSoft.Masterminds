using AutoMapper;
using Microsoft.AspNetCore.Identity;
using NeoSoft.Masterminds.Domain.Interfaces;
using NeoSoft.Masterminds.Domain.Models.Entities.Identity;
using NeoSoft.Masterminds.Domain.Models.Exceptions;
using NeoSoft.Masterminds.Domain.Models.Models;
using NeoSoft.Masterminds.Services.Interfaces;
using System.Threading.Tasks;

namespace NeoSoft.Masterminds.Infrastructure.Business
{
    public class UserProfileService : IUserProfileService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IProfileRepository _profileRepository;

        public UserProfileService(UserManager<AppUser> userManager, IProfileRepository profileRepository)
        {
            _userManager = userManager;
            _profileRepository = profileRepository;
        }

        public async Task<UserProfileModel> GetProfileByEmail(string email)
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

            var userProfile = new UserProfileModel()
            {
                Email = email,
                FirstName = profileEntity.ProfileFirstName,
                LastName = profileEntity.ProfileLastName

            };
            return userProfile;
        }
    }
}
