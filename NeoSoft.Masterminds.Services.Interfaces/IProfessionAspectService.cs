using NeoSoft.Masterminds.Domain.Models.Filters;
using NeoSoft.Masterminds.Domain.Models.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace NeoSoft.Masterminds.Services.Interfaces
{
    public interface IProfessionAspectService
    {
        Task<List<ProfessionalAspectModel>> GetAllAsp(ProfessionalAspectSearchFilter filter);
    }
}
