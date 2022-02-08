using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NeoSoft.Masterminds.Domain.Models.Filters;
using NeoSoft.Masterminds.Domain.Models.Models;
using NeoSoft.Masterminds.Domain.Models.Responses;
using NeoSoft.Masterminds.Models.Incoming.Filters;
using NeoSoft.Masterminds.Models.Outcoming;
using NeoSoft.Masterminds.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NeoSoft.Masterminds.Controllers
{
    [Route("api/profesional-aspect")]
    [ApiController]
    public class ProfessionalAspectController : Controller
    {
        private readonly ILogger<ProfessionalAspectController> _logger;
        private readonly IMapper _mapper;
        private IProfessionAspectService _professionalAspectService;
       
        public ProfessionalAspectController(ILogger<ProfessionalAspectController> logger, IMapper mapper, IProfessionAspectService service)
        {
            _professionalAspectService = service;
            _mapper = mapper;
            _logger = logger;

        }

        [HttpGet]
        public async Task<ApiResponse<List<ProfessionalAspectViewModel>>> GetAllAsp([FromQuery] ProfAspFilterApiModel filter)
        {
            _logger.LogInformation("Get professionalAspect action started");

            var professionList = await _professionalAspectService.GetAllAsp(_mapper.Map<ProfessionalAspectSearchFilter>(filter));
            _logger.LogInformation($"Get professionalAspect action finished successfuly");

            return _mapper.Map<List<ProfessionalAspectViewModel>>(professionList);

        }
    }
}
