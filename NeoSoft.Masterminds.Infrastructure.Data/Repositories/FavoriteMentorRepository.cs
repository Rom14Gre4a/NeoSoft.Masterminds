using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NeoSoft.Masterminds.Domain.Interfaces;
using NeoSoft.Masterminds.Domain.Models.Entities;
using NeoSoft.Masterminds.Domain.Models.Entities.Identity;
using NeoSoft.Masterminds.Domain.Models.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeoSoft.Masterminds.Infrastructure.Data.Repositories
{
    public class FavoriteMentorRepository : IFavoriteMentorRepository
    {
        private readonly MastermindsDbContext _dbContext;
        private readonly ILogger<FavoriteMentorRepository> _logger;
        public FavoriteMentorRepository(ILogger<FavoriteMentorRepository> logger, MastermindsDbContext dbContext)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<List<MentorEntity>> GetFavoriteMentorsAsync(int profileId)
        {
           
            var favoriteMentors = await _dbContext.Profiles.Where(p => p.Id == profileId).SelectMany(p => p.Favorites).ToListAsync();

            return favoriteMentors;
        }

        public async Task<AppUser>  GetFavoriteUser(string email)
        {
            var favorite = await _dbContext.Users
                .Include(u => u.Profile).ThenInclude(m => m.Favorites).ThenInclude(m => m.Profile)
                .Include(u => u.Profile).ThenInclude(m => m.Favorites).ThenInclude(m => m.Professions)
                .Include(u => u.Profile).ThenInclude(m => m.RecivedReviews)
                .FirstOrDefaultAsync(u => u.Email == email);

            if (favorite == null)
            {
                throw new NotFoundException($"No user With email = {email} founded.");
            }

            return favorite;
        }
        public async Task<List<MentorEntity>> Update(int profileId)
        {
            var favoriteMentors = await _dbContext.Profiles.Where(p => p.Id == profileId).SelectMany(p => p.Favorites).ToListAsync();

            return favoriteMentors;
        }
        public async Task<int> GetProfileTotalFavorites(int profileId)
        {
            var totalFavorites = await _dbContext.Profiles.Where(p => p.Id == profileId).SelectMany(p => p.Favorites).CountAsync();
            return totalFavorites;
        }
    }
}
