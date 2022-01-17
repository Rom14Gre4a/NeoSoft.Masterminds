using Microsoft.EntityFrameworkCore;
using NeoSoft.Masterminds.Domain.Interfaces;
using NeoSoft.Masterminds.Domain.Models.Entities;
using NeoSoft.Masterminds.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NeoSoft.Masterminds.Domain.Models;
using System.Linq;
using NeoSoft.Masterminds.Domain.Models.Enums;

namespace NeoSoft.Masterminds.Infrastructure.Data.Repositories
{
    public class MentorRepository : IMentorRepository
    {
        private readonly MastermindsDbContext _dbContext;

        public MentorRepository(MastermindsDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<MentorEntity> GetMentorProfileById(int mentorId)
        {  var mentor = await _dbContext.Mentors.Include(p => p.Profile).ThenInclude(r => r.SentReviews)
                .FirstOrDefaultAsync(m => m.Id == mentorId);
            //var mentor =  _dbContext
            //    .Mentors
            //    .Include(x => x.Profile)
            //    .Include(x => x.Reviews).ThenInclude(x => x.FromProfile)
            //    .FirstOrDefaultAsync(x => x.Id == mentorId);

            return mentor;
            //var mentor = await _dbContext.Mentors.FirstOrDefaultAsync(x => x.Id == mentorId);
            //return mentor;
        }
        public async Task<List<MentorEntity>> Get(GetFilter filter)
        {
            var baseQuery = _dbContext.Mentors.AsNoTracking();

            if (filter.OrderByProperty != null)
            {
                switch (filter.OrderByProperty)
                {
                    case nameof(MentorEntity.Profile.Id):
                        baseQuery = filter.SortOrder == SortOrder.Asc
                            ? baseQuery.OrderBy(x => x.Id)
                            : baseQuery.OrderByDescending(x => x.Id);
                        break;

                    case nameof(MentorEntity.Profile.ProfileLastName):
                        baseQuery = filter.SortOrder == SortOrder.Asc
                            ? baseQuery.OrderBy(x => x.Profile.ProfileLastName)
                            : baseQuery.OrderByDescending(x => x.Profile.ProfileLastName);
                        break;

                    default:
                        baseQuery = filter.SortOrder == SortOrder.Asc
                            ? baseQuery.OrderBy(x => x.Profile.ProfileFirstName)
                            : baseQuery.OrderByDescending(x => x.Profile.ProfileFirstName);
                        break;
                }
            }

            if (!string.IsNullOrWhiteSpace(filter.SearchText))
            {
                baseQuery = baseQuery.Where(x =>
                    x.Profile.ProfileFirstName.Contains(filter.SearchText) || 
                    x.Profile.ProfileLastName.Contains(filter.SearchText) ||
                    x.Specialty.Contains(filter.SearchText));
            }

            var mentors = await baseQuery
                .Include(x => x.Profile).ThenInclude(x => x.Photo)
                .Skip(filter.Skip)
                .Take(filter.Take)
                .ToListAsync();

            return mentors;
        }
    }



    
}
