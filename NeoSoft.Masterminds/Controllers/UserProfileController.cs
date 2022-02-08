using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NeoSoft.Masterminds.Domain.Models.Responses;
using NeoSoft.Masterminds.Models.Outcoming;
using NeoSoft.Masterminds.Services.Interfaces;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NeoSoft.Masterminds.Controllers
{
    [Route("api/user-profile")]
    [ApiController]
    public class UserProfileController : Controller
    {
        private readonly IUserProfileService _service;
        private readonly IMapper _mapper;

        public UserProfileController(IUserProfileService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

       // [Authorize]
        [HttpGet]
        public async Task<ApiResponse<UserProfileApiModel>> UserProfile(string email)
        {
            if (User.Identity.IsAuthenticated)
            { 
                email = User.FindFirstValue(ClaimTypes.Email);
            }
           

            var userProfile = await _service.GetProfileByEmail(email);

            return _mapper.Map<UserProfileApiModel>(userProfile);
        }
    }
}
