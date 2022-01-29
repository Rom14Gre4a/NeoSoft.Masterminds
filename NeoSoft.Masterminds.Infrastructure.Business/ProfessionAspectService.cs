using NeoSoft.Masterminds.Domain.Interfaces;
using NeoSoft.Masterminds.Domain.Models.Filters;
using NeoSoft.Masterminds.Domain.Models.Models;
using NeoSoft.Masterminds.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NeoSoft.Masterminds.Infrastructure.Business
{
    public class ProfessionAspectService : IProfessionAspectService
    {
       
             private readonly IProfessionalAspectRepository _professionalAspectRepository;

        public ProfessionAspectService(IProfessionalAspectRepository professionalAspectRepository)
        {
            _professionalAspectRepository = professionalAspectRepository;
        }
        public async Task<List<ProfessionalAspectModel>> GetAllAsp(ProfessionalAspectSearchFilter filter)
        {
            var professionAspListDb = await _professionalAspectRepository.GetAllAsp(filter);
            var list = new List<ProfessionalAspectModel>();
            foreach (var professional in professionAspListDb)
            {
                list.Add(new ProfessionalAspectModel
                {
                    Id = professional.Id,
                    Aspect = professional.Aspect,
                });
            }

            return list;
        }
    }

    
}
