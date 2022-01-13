using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NeoSoft.Masterminds.Infrastructure.Data;
using NeoSoft.Masterminds.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace NeoSoft.Masterminds.Controllers
{
    [Route("api/mentor")]
    [ApiController]
    public class MentorController : ControllerBase
    {
        private readonly ILogger<MentorController> _logger;
        private readonly IMentorService _mentorService;
    
        public MentorController(ILogger<MentorController> logger, IMentorService mentorService)
        {
            _logger = logger;
            _mentorService = mentorService;
        }
        [HttpGet("Mentor")]
        public async Task<IActionResult> GetMentorProfileById( int mentorId)
        {
            var mentorModel = await _mentorService.GetMentorProfileById(mentorId);

            return Ok(mentorModel);
        }
    }
}
