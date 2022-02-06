using AutoMapper;
using NeoSoft.Masterminds.Domain.Interfaces;
using NeoSoft.Masterminds.Domain.Models.Filters;
using NeoSoft.Masterminds.Domain.Models.Models;
using NeoSoft.Masterminds.Infrastructure.Data;
using NeoSoft.Masterminds.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NeoSoft.Masterminds.Infrastructure.Business
{
    public class ProfessionService : IProfessionService
    {
        private readonly IProfessionRepository _professionRepository;
        private readonly IMapper _mapper;
        public ProfessionService(IProfessionRepository professionRepository, IMapper mapper)
        {
            _professionRepository = professionRepository;
            _mapper = mapper;
        }
        public async Task<List<ProfessionsModel>> GetAll(ProfessionFilter filter)
        {
            var professionListDb = await _professionRepository.GetAll(filter);
            return _mapper.Map<List<ProfessionsModel>>(professionListDb);

            //var list = new List<ProfessionsModel>();
            //foreach (var profession in professionListDb)
            //{
            //    list.Add(new ProfessionsModel
            //    {
            //        Id = profession.Id,
            //        Name = profession.Name,
            //    });
            //}

            //return list;
        }
    }
}
