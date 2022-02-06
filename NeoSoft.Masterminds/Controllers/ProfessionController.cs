using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NeoSoft.Masterminds.Domain.Models.Filters;
using NeoSoft.Masterminds.Domain.Models.Responses;
using NeoSoft.Masterminds.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using NeoSoft.Masterminds.Models.Incoming.Filters;
using NeoSoft.Masterminds.Models.Outcoming;

namespace NeoSoft.Masterminds.Controllers
{
    [Route("api/profession")]
    [ApiController]
    public class ProfessionController : Controller
    {
        private readonly ILogger<ProfessionController> _logger;
        private IProfessionService _professionService;
        private readonly IMapper _mapper;

        public ProfessionController(ILogger<ProfessionController> logger, IMapper mapper, IProfessionService service)
        {
            _professionService = service;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ApiResponse<List<ProfessionViewModel>>> GetAll([FromQuery] ProfFilterApiModel filter)
        {

            var professionList = await _professionService.GetAll(_mapper.Map<ProfessionFilter>(filter));
            return _mapper.Map<List<ProfessionViewModel>>(professionList);

        }
    }
}
