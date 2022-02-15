using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NeoSoft.Masterminds.Domain.Models.Responses;
using NeoSoft.Masterminds.Models.Outcoming;
using NeoSoft.Masterminds.Services.Interfaces;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NeoSoft.Masterminds.Controllers
{
    [Route("api")]
    [ApiController]
    public class FavoriteController : Controller
    {
       
        private readonly IFavoriteMentorService _service;
        private readonly IMapper _mapper;


        public FavoriteController(IFavoriteMentorService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet("favorites")]
        public async Task<ApiResponse<List<MentorListView>>> GetAllFavorites(string email)
        {
            if (User.Identity.IsAuthenticated)
            {
                email = User.FindFirstValue(ClaimTypes.Email);
            }
            var mentorList = await _service.GetAll(email);
          
            return _mapper.Map<List<MentorListView>>(mentorList);

        }

        [Authorize]
        [HttpPut("add-remove-favorite")]
        public async Task<ApiResponse<bool>> UpdateFavorites(int mentorId)
        {

            var email = User.FindFirstValue(ClaimTypes.Name);
            
            var user = await _service.GetUserByEmail(email);
            var a = await _service.UpdateFavorites(user, mentorId);
            return a;

        }

        [Authorize]
        [HttpGet("favorites-count")]
        public async Task<ApiResponse<int>> FavoritesCount(int mentorId)
        {
           
            var favoriteCount = await _service.FavoritesCount(mentorId);
            return favoriteCount;
        }
    }
}
