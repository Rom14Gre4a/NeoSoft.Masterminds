using NeoSoft.Masterminds.Domain.Interfaces;
using NeoSoft.Masterminds.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NeoSoft.Masterminds.Infrastructure.Data.Repositories
{  
        public class ProfileRepository : IProfileRepository
        {
            private readonly MastermindsDbContext _context;

            public ProfileRepository(MastermindsDbContext context)
            {
                _context = context;
            }

            public async Task<ProfileEntity> GetProfileById(int id)
            {
                return await _context.Profiles.FindAsync(id);
            }
        }
}
