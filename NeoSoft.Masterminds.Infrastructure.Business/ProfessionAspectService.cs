using AutoMapper;
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
         private readonly IMapper _mapper;
        public ProfessionAspectService(IProfessionalAspectRepository professionalAspectRepository, IMapper mapper)
        {
            _professionalAspectRepository = professionalAspectRepository;
            _mapper = mapper;
        }
        public async Task<List<ProfessionalAspectModel>> GetAllAsp(ProfessionalAspectSearchFilter filter)
        {
            var professionAspListDb = await _professionalAspectRepository.GetAllAsp(filter);
            return _mapper.Map<List<ProfessionalAspectModel>>(professionAspListDb); 
        }
    }

    
}
