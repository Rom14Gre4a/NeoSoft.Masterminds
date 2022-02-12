using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NeoSoft.Masterminds.Domain.Models.Models.Auth;
using NeoSoft.Masterminds.Domain.Models.Responses;
using NeoSoft.Masterminds.Models.Incoming;
using NeoSoft.Masterminds.Models.Outcoming;
using NeoSoft.Masterminds.Models.Registration;
using NeoSoft.Masterminds.Services.Interfaces;
using System.Threading.Tasks;

namespace NeoSoft.Masterminds.Controllers
{
    [Route("api/account")]
    [ApiController]
     public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        public AccountController(ILogger<AccountController> logger, IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost("login")]
       
        public async Task<ApiResponse<TokenApiModel>> Login(IncomLogin login)
        {
            var token = await _accountService.Login(new Login
            {
                Email = login.Email,
                Password = login.Password
            });
            //var token = await _accountService.Login(_mapper.Map<Login>(login));

            return  _mapper.Map<TokenApiModel>(token);
           
        }

        [HttpPost("create-user")]

        public async Task<ApiResponse<TokenApiModel>> CreateNewUser(IncomUserRegistration registration)
        {
            _logger.LogInformation("Registration user action started");

            var registeredUser = new UserRegistration
            {
                Email = registration.Email,
                FirstName = registration.FirstName,
                LastName = registration.LastName,
                Password = registration.Password,
                ConfirmPassword = registration.ConfirmPassword,
                PhotoId = registration.PhotoId
            };

            var token = await _accountService.CreateNewUserAccountAsync(registeredUser);

            _logger.LogInformation("Registration user action finished successfuly");

            return _mapper.Map<TokenApiModel>(token);
        }

        [HttpPost("create-mentor")]
        public async Task<ApiResponse<TokenApiModel>> CreateNewMentor(IncomMentorRegistration registration)
        {
            _logger.LogInformation("create mentor action started");

            var registeredMentor = _mapper.Map<MentorRegistration>(registration);

            var token = await _accountService.CreateNewMentorAccount(registeredMentor);

            _logger.LogInformation("create mentor action finished successfuly");

            return _mapper.Map<TokenApiModel>(token);

        }

        [HttpPost("change-password")]
        public async Task<ApiResponse<TokenApiModel>> ChangePassword(ChangePasswordModel model)
        {
            var newPassword = new ChangePassword
            {
                Email = model.Email,
                CurrentPassword = model.CurrentPassword,
                NewPassword = model.NewPassword
            };
            var token = await _accountService.ChangePassword(newPassword);
            return _mapper.Map<TokenApiModel>(token);
        }
    }
}
