using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NeoSoft.Masterminds.Domain.Models.Filters;
using NeoSoft.Masterminds.Domain.Models.Responses;
using NeoSoft.Masterminds.Models;
using NeoSoft.Masterminds.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NeoSoft.Masterminds.Controllers
{
    [Route("api/profession")]
    [ApiController]
    public class ProfessionController : Controller
    {
        private readonly ILogger<ProfessionController> _logger;
        private IProfessionService _professionService;

        public ProfessionController(ILogger<ProfessionController> logger, IProfessionService service)
        {
            _professionService = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ApiResponse<List<ProfessionViewModel>>> GetAll([FromQuery] ProfessionFilter filter)
        {
            _logger.LogInformation("Get profession action started");

            var professionList = await _professionService.GetAll(new ProfessionFilter
            {
                Skip = filter.Skip,
                Take = filter.Take,
                OrderByProperty = filter.OrderByProperty,
                SearchText = filter.SearchText,
                SortOrder = filter.SortOrder,
            });

            var pro = new List<ProfessionViewModel>();

            foreach (var ProfessionDb in professionList)
            {
                pro.Add(new ProfessionViewModel
                {
                    Id = ProfessionDb.Id,
                    Name = ProfessionDb.Name,
                });
            }
            _logger.LogInformation($"Get profession action finished successfuly");

            return pro;

        }
    }
}
