using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NeoSoft.Masterminds.Domain.Models;
using NeoSoft.Masterminds.Domain.Models.Enums;
using NeoSoft.Masterminds.Infrastructure.Data;
using NeoSoft.Masterminds.Models;
using NeoSoft.Masterminds.Services.Interfaces;
using System;
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
    
        public MentorController(ILogger<MentorController> logger, IMentorService mentorService)
        {
            _logger = logger;
            _mentorService = mentorService;
        }
        [HttpGet("Mentor")]
        public async Task<IActionResult> GetMentorProfileById( int mentorId)
        {
            var mentorModel = await _mentorService.GetMentorProfileById(mentorId);
            if (mentorModel == null)
                return null;
            return Ok(mentorModel);
            
        }

        [HttpGet]
        public async Task<List<MentorListView>> Get(int skip = 0, int take = 15)
        {
            var mentors = await _mentorService.Get(skip, take);
            {

            }

          
            return mentors.Select(x => new MentorListView
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Specialty = x.Specialty,
                Rating = x.Rating,
                //ProfilePhoto = GetPhotoPath(x.ProfilePhotoId)
            }).ToList();
        }
    }
}
