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
        public async Task<List<MentorEntity>> Get(int skip = 0, int take = 15)
        {
            var mentors = await _dbContext.Mentors.Include(p => p.Profile)
                .Skip(skip).Take(take).ToListAsync();
            return mentors;
        }



    }
}
