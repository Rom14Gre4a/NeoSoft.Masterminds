using Microsoft.EntityFrameworkCore;
using NeoSoft.Masterminds.Domain.Interfaces;
using NeoSoft.Masterminds.Domain.Models.Entities;
using NeoSoft.Masterminds.Domain.Models.Enums;
using NeoSoft.Masterminds.Domain.Models.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeoSoft.Masterminds.Infrastructure.Data
{
    public class ProfessionRepository : IProfessionRepository
    {
        private MastermindsDbContext _context;
        public ProfessionRepository(MastermindsDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProfessionEntity>> GetAll(ProfessionFilter filter)
        {
            var baseQuery = _context.Professions.AsNoTracking();

            switch (filter.OrderByProperty)
            {
                case nameof(ProfessionalAspectEntity.Id):
                    baseQuery = filter.SortOrder == SortOrder.Ascending
                        ? baseQuery.OrderBy(x => x.Id)
                        : baseQuery.OrderByDescending(x => x.Id);
                    break;

                default:
                    baseQuery = filter.SortOrder == SortOrder.Ascending
                        ? baseQuery.OrderBy(x => x.Name)
                        : baseQuery.OrderByDescending(x => x.Name);
                    break;
            }
            if (!string.IsNullOrWhiteSpace(filter.SearchText))
            {
                baseQuery = baseQuery.Where(p => p.Name.Contains(filter.SearchText));
            }

            var professions = await baseQuery.Skip(filter.Skip)
                .Take(filter.Take).ToListAsync();
            return professions;
        }
    }
}
   
