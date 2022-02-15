using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NeoSoft.Masterminds.Domain.Interfaces;
using NeoSoft.Masterminds.Domain.Models.Entities;
using NeoSoft.Masterminds.Domain.Models.Entities.Identity;
using NeoSoft.Masterminds.Domain.Models.Exceptions;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<ProfileEntity> GetFavoriteMentorAsync(int profileId)
        {
            var favoriteMentor = await _dbContext.Mentors.Where(p => p.Id == profileId).SelectMany(p => p.fans).FirstOrDefaultAsync();

            return favoriteMentor;
        }

        public async Task<AppUser> GetFavoriteUser(string email)
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
        public async Task<ProfileEntity> GetFavoriteProfile(string email)
        {
            return await _dbContext.Profiles.FindAsync(email);
        }

        public async Task AddFavorite(ProfileEntity profile, int mentorId)
        {
            var favoriteMentor = await _dbContext.Mentors.Where(p => p.Id == mentorId)
                .FirstOrDefaultAsync(profile => profile.Id == mentorId);

            if (favoriteMentor == null)
            {
                throw new NotFoundException($"Mentor with id = {mentorId} not found!");
            }

            profile.Favorites.Add(favoriteMentor);

            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveFavorite(ProfileEntity profile, int mentorId)
        {
            var favoriteMentor = await _dbContext.Mentors.Where(p => p.Id == mentorId)
               .FirstOrDefaultAsync(profile => profile.Id == mentorId);

            if(favoriteMentor == null)
            {
                throw new NotFoundException($"Mentor with id = {mentorId} not found!");
            }
            var deleteFavorite = profile.Favorites.Remove(favoriteMentor);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<int> GetProfileTotalFavorites(int profileId)
        {
            var totalFavorites = await _dbContext.Profiles.Where(p => p.Id == profileId).SelectMany(p => p.Favorites).CountAsync();
            return totalFavorites;
        }
    }
}
