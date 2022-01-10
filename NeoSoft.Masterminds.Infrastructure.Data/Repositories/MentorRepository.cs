using Microsoft.EntityFrameworkCore;
using NeoSoft.Masterminds.Domain.Interfaces;
using NeoSoft.Masterminds.Domain.Models.Entities;
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

        public async Task<bool> AddProfile(ProfileEntity profile)
        {
            _dbContext.Profiles.Add(profile);
            await _dbContext.SaveChangesAsync();

            return true;
        }
        public async Task<ProfileEntity> GetProfile(int profileId)
        {
            var profile = await _dbContext.Profiles.FirstOrDefaultAsync(x => x.Id == profileId);
            return profile;
        }
     

       
    }
}
