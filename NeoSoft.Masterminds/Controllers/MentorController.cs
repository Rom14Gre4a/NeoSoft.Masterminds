using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NeoSoft.Masterminds.Domain.Models.Enums;
using NeoSoft.Masterminds.Domain.Models.Filters;
using NeoSoft.Masterminds.Domain.Models.Responses;
using NeoSoft.Masterminds.Models;
using NeoSoft.Masterminds.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeoSoft.Masterminds.Controllers
{
    [Route("api/mentor")]
    [ApiController]
    public class MentorController : ControllerBase
    {
        private readonly ILogger<MentorController> _logger;
        private readonly IMentorService _mentorService;
        private readonly IMapper _mapper;

        public MentorController(ILogger<MentorController> logger, IMentorService mentorService, IMapper mapper)
        {
            _logger = logger;
            _mentorService = mentorService;
            _mapper = mapper;
        }
        [HttpGet("Id")]
        public async Task<ApiResponse<MentorView>> GetMentorProfileById( int mentorId)
        {
            _logger.LogInformation("Get mentor action started");

            var mentorModel = await _mentorService.GetMentorProfileById(mentorId);

            _logger.LogInformation($"Get mentor action finished successfuly. Requested mentor ID is {mentorModel.Id}");

            return new MentorView
            {
                Id = mentorModel.Id,
                FirstName = mentorModel.FirstName,
                LastName = mentorModel.LastName,
                Description = mentorModel.Description,
                HourlyRate = mentorModel.HourlyRate,
                Rating = mentorModel.Rating,
                Professions = mentorModel.Professions,
                ProfessionalAspects = mentorModel.ProfessionalAspects,
                ReviewsTotalCount = mentorModel.ReviewsTotalCount,
                ProfilePhoto = GetPhotoPath(mentorModel.ProfilePhotoId),
                Reviews = mentorModel.Reviews.Select(x => new ReviewView
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Rating = x.Rating,
                    Text = x.Text,
                    ProfilePhoto = GetPhotoPath(x.ProfilePhotoId)
                }).ToList()
               
            };
        }

        [HttpGet("List")]
        public async Task<ApiResponse<List<MentorListView>>> Get([FromQuery] GetListItems filter)
        {
            _logger.LogInformation("Get mentors action started");

            var mentors = await _mentorService.Get(new GetFilter
            {
                Skip = filter.Skip ?? 0,
                Take = filter.Take ?? 15,
                OrderByProperty = string.Empty,
                SearchText = filter.SearchText,
                SortOrder = filter.SortOrder ?? SortOrder.Descending
            });

            _logger.LogInformation("Get mentors action finished successfuly");


            return mentors.Select(x => new MentorListView
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Rating = x.Rating,
                Professions = (IList<ProfessionViewModel>)x.Professions,
                ProfilePhoto = GetPhotoPath(x.ProfilePhotoId)
            }).ToList();
        }

        private string GetPhotoPath(int profilePhotoId)
        {
            // https://localhost:5001/api/file/3
            return $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}/api/file/{profilePhotoId}";
        }
    }
}
