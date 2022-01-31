using Microsoft.AspNetCore.Mvc;
using NeoSoft.Masterminds.Domain.Models.Models.Auth;
using NeoSoft.Masterminds.Domain.Models.Responses;
using NeoSoft.Masterminds.Models;
using NeoSoft.Masterminds.Models.Registration;
using NeoSoft.Masterminds.Services.Interfaces;
using System.Threading.Tasks;

namespace NeoSoft.Masterminds.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("login")]
        public async Task<ApiResponse<TokenApiModel>> Login(IncomLogin login)
        {
            var token = await _accountService.Login(new Login
            {
                Email = login.Email,
                Password = login.Password
            });

            return new TokenApiModel
            {
                AccessToken = token.AccessToken
            };
        }

        [HttpPost("create-user")]


        public async Task<ApiResponse<TokenApiModel>> CreateNewUser(IncomUserRegistration registration)
        {
            var registeredUser = new UserRegistration
            { 
                Email = registration.Email,
                FirstName = registration.FirstName,                                           
                LastName = registration.LastName,
                Password = registration.Password,
                ConfirmPassword = registration.ConfirmPassword,
                PhotoId = registration.PhotoId
            };

            var token = await _accountService.CreateNewUserAccount(registeredUser);

           return new TokenApiModel
           { 
                 AccessToken = token.AccessToken 
           };  
        }

        [HttpPost("create-mentor")]
        public async Task<ApiResponse<TokenApiModel>> CreateNewMentor(IncomMentorRegistration registration)
        {
            var registeredMentor = new MentorRegistration
            {
                Email = registration.Email,
                FirstName = registration.FirstName,
                LastName = registration.LastName,
                Password = registration.Password,
                ConfirmPassword = registration.ConfirmPassword,
                PhotoId = registration.PhotoId
            };

            var token = await _accountService.CreateNewMentorAccount(registeredMentor);

            return new TokenApiModel
            {
                AccessToken = token.AccessToken
            };

        }
    }
}
