
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

namespace NeoSoft.Masterminds.Infrastructure.Data.Repositories
{
    public class ProfessionalAspectRepository: IProfessionalAspectRepository
    {
        private MastermindsDbContext _context;
        public ProfessionalAspectRepository(MastermindsDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProfessionalAspectEntity>> GetAllAsp(ProfessionalAspectSearchFilter filter)
        {
            var baseQuery = _context.ProfessionalAspects.AsNoTracking();

            switch (filter.OrderByProperty)
            {
                case nameof(ProfessionalAspectEntity.Id):
                    baseQuery = filter.SortOrder == SortOrder.Ascending
                        ? baseQuery.OrderBy(x => x.Id)
                        : baseQuery.OrderByDescending(x => x.Id);
                    break;

                default:
                    baseQuery = filter.SortOrder == SortOrder.Ascending
                        ? baseQuery.OrderBy(x => x.Aspect)
                        : baseQuery.OrderByDescending(x => x.Aspect);
                    break;
            }
            if (!string.IsNullOrWhiteSpace(filter.SearchText)) 
            {
                baseQuery = baseQuery.Where(p => p.Aspect.Contains(filter.SearchText));
            }

            var professions = await baseQuery.Skip(filter.Skip)
                .Take(filter.Take).ToListAsync();
            return professions;
        }
    }
}
