using Microsoft.AspNetCore.Mvc;
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
        private IProfessionService _professionService;

        public ProfessionController(IProfessionService service)
        {
            _professionService = service;
        }


        [HttpGet]
        public async Task<ApiResponse<List<ProfessionViewModel>>> GetAll([FromQuery] ProfessionFilter filter)
        {
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
            return pro;

        }
    }
}
