using Microsoft.AspNetCore.Mvc;
using NeoSoft.Masterminds.Domain.Models.Filters;
using NeoSoft.Masterminds.Domain.Models.Models;
using NeoSoft.Masterminds.Domain.Models.Responses;
using NeoSoft.Masterminds.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NeoSoft.Masterminds.Controllers
{
    [Route("api/profesional-aspect")]
    [ApiController]
    public class ProfessionalAspectController : Controller
    {
        private IProfessionAspectService _professionalAspectService;
       
        public ProfessionalAspectController(IProfessionAspectService service)
        {
            _professionalAspectService = service;
        }

        [HttpGet]
        public async Task<ApiResponse<List<ProfessionalAspectModel>>> GetAllAsp([FromQuery] ProfessionalAspectSearchFilter filter)
        {
            var professionList = await _professionalAspectService.GetAllAsp(new ProfessionalAspectSearchFilter
            {
                Skip = filter.Skip,
                Take = filter.Take,
                OrderByProperty = filter.OrderByProperty,
                SearchText = filter.SearchText,
                SortOrder = filter.SortOrder,

            });

            var pro = new List<ProfessionalAspectModel>();

            foreach (var ProfessionDb in professionList)
            {
                pro.Add(new ProfessionalAspectModel
                {
                    Id = ProfessionDb.Id,
                    Aspect = ProfessionDb.Aspect,
                });
            }
            return pro;

        }
    }
}
