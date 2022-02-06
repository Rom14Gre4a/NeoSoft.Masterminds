using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NeoSoft.Masterminds.Domain.Models.Enums;
using NeoSoft.Masterminds.Domain.Models.Filters;
using NeoSoft.Masterminds.Domain.Models.Responses;
using NeoSoft.Masterminds.MapConfig;
using NeoSoft.Masterminds.Models;
using NeoSoft.Masterminds.Models.Incoming.Filters;
using NeoSoft.Masterminds.Models.Outcoming;
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

            return MentorMapModel.Map(mentorModel, HttpContext.Request);
           

        }
        

        [HttpGet("List")]
        public async Task<ApiResponse<List<MentorListView>>> Get([FromQuery] MentorFilterApiModel filter)
        {
            _logger.LogInformation("Get mentors action started");

            var mentors = await _mentorService.Get(_mapper.Map<MentorSearchFilter>(filter));
           
            _logger.LogInformation("Get mentors action finished successfuly");


            return _mapper.Map<List<MentorListView>>(mentors);
        }
    
    }
}
