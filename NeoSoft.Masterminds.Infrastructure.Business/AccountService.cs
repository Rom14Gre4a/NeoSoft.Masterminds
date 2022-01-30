using Microsoft.AspNetCore.Identity;
using NeoSoft.Masterminds.Domain.Models.Entities;
using NeoSoft.Masterminds.Domain.Models.Entities.Identity;
using NeoSoft.Masterminds.Domain.Models.Models.Auth;
using NeoSoft.Masterminds.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NeoSoft.Masterminds.Infrastructure.Business
{
    public class AccountService : IAccountService
    {
        private readonly IJwtTokenService _jwtTokenService;
        private readonly UserManager<AppUser> _userManager;

        public AccountService(IJwtTokenService jwtTokenService, UserManager<AppUser> userManager)
        {
            _jwtTokenService = jwtTokenService;
            _userManager = userManager;
        }

        public async Task<TokenModel> CreateNewUserAccount(UserRegistration registration)
        {
            AppUser user = new AppUser
            {
                Email = registration.Email,
                UserName = registration.Email,
                Profile = new ProfileEntity
                {
                    ProfileFirstName = registration.FirstName,
                    ProfileLastName = registration.LastName,
                    PhotoId = registration.PhotoId
                }
            };

            var createNewUserResult = await _userManager.CreateAsync(user, registration.Password);
            if (createNewUserResult.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");

                var roles = await _userManager.GetRolesAsync(user);
                var token = _jwtTokenService.CreateAccessToken(user, roles);
                
                var jwttoken = new TokenModel { AccessToken = token };
                return jwttoken;
            }
            return new TokenModel();

        }

        public async Task<TokenModel> CreateNewMentorAccount(MentorRegistration registration)
        {
            AppUser mentor = new AppUser
            {
                Email = registration.Email,
                UserName = registration.Email,
                Profile = new ProfileEntity
                {
                    ProfileFirstName = registration.FirstName,
                    ProfileLastName = registration.LastName,
                    PhotoId = registration.PhotoId == 0 ? null : registration.PhotoId,
                }
            };

            var createNewMentorResult = await _userManager.CreateAsync(mentor, registration.Password);
            if (createNewMentorResult.Succeeded)
            {
                await _userManager.AddToRoleAsync(mentor, "Mentor");
                var roles = await _userManager.GetRolesAsync(mentor);
                var token = _jwtTokenService.CreateAccessToken(mentor, roles);
                
                var jwttoken = new TokenModel { AccessToken = token };
                return jwttoken;
            }
           return new TokenModel();  
        }

        public async Task<TokenModel> Login(Login login)
        {
            var registeredUser = await _userManager.FindByEmailAsync(login.Email);
            bool passCorrect = await _userManager.CheckPasswordAsync(registeredUser, login.Password);

            if (registeredUser != null && passCorrect)
            { 
                var roles = await _userManager.GetRolesAsync(registeredUser);
                var token = _jwtTokenService.CreateAccessToken(registeredUser, roles);

                var jwttoken = new TokenModel { AccessToken = token };
                return jwttoken;
            }
           return new TokenModel();
        }
    }
}
