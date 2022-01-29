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

        public ProfessionService(IProfessionRepository professionRepository)
        {
            _professionRepository = professionRepository;
        }
        public async Task<List<ProfessionsModel>> GetAll(ProfessionFilter filter)
        {
            var professionListDb = await _professionRepository.GetAll(filter);
            var list = new List<ProfessionsModel>();
            foreach (var profession in professionListDb)
            {
                list.Add(new ProfessionsModel
                {
                    Id = profession.Id,
                    Name = profession.Name,
                });
            }

            return list;
        }
    }
}
