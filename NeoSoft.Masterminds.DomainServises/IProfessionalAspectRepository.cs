using NeoSoft.Masterminds.Domain.Models.Entities;
using NeoSoft.Masterminds.Domain.Models.Filters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NeoSoft.Masterminds.Domain.Interfaces
{
    public interface IProfessionalAspectRepository
    {
        public Task<List<ProfessionalAspectEntity>> GetAllAsp(ProfessionalAspectSearchFilter filter);
    }
}
