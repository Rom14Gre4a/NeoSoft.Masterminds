using Microsoft.EntityFrameworkCore;
using NeoSoft.Masterminds.Domain.Interfaces;
using NeoSoft.Masterminds.Domain.Models.Entities;
using NeoSoft.Masterminds.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NeoSoft.Masterminds.Infrastructure.Data.Repositories
{
    public class MentorRepository : IMentorRepository
    {
        private readonly MastermindsDbContext _dbContext;

        public MentorRepository(MastermindsDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<MentorEntity> GetMentorProfileById(int mentorId)
        { var mentor =  _dbContext
                .Mentors
                .Include(x => x.Profile)
                .Include(x => x.Reviews).ThenInclude(x => x.Owner)
                .FirstOrDefaultAsync(x => x.Id == mentorId);

            return mentor;
            //var mentor = await _dbContext.Mentors.FirstOrDefaultAsync(x => x.Id == mentorId);
            //return mentor;
        }



    }
}
