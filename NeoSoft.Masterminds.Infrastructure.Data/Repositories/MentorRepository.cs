using Microsoft.EntityFrameworkCore;
using NeoSoft.Masterminds.Domain.Interfaces;
using NeoSoft.Masterminds.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using NeoSoft.Masterminds.Domain.Models.Enums;
using NeoSoft.Masterminds.Domain.Models.Filters;
using Microsoft.Extensions.Logging;

namespace NeoSoft.Masterminds.Infrastructure.Data.Repositories
{
    public class MentorRepository : IMentorRepository
    {
        private readonly MastermindsDbContext _dbContext;
        private readonly ILogger<MentorRepository> _logger;

        public MentorRepository(ILogger<MentorRepository> logger, MastermindsDbContext dbContext)
        {
            _dbContext = dbContext;
            _logger = logger;
        }
        public async Task<MentorEntity> GetMentorProfileById(int mentorId)
        {
            _logger.LogInformation("Get mentor by Id in repository started");

            var mentor = await _dbContext
                .Mentors
                .Include(m => m.ProfessionalAspects)
                .Include(m => m.Professions)
                .Include(x => x.Profile).ThenInclude(x => x.RecivedReviews)
                .Include(x => x.Profile).ThenInclude(x => x.Photo)
                .FirstOrDefaultAsync(x => x.Id == mentorId);

            _logger.LogInformation("Get mentor by Id in repository finished successfuly");

            return mentor;
        }
        public async Task<List<MentorEntity>> Get(GetFilter filter)
        {
            _logger.LogInformation("Getting a list of filter mentors in the repository has started");

            var baseQuery = _dbContext.Mentors.AsNoTracking();

            if (filter.OrderByProperty != null)
            {
                switch (filter.OrderByProperty)
                {
                    case nameof(MentorEntity.Profile.Id):
                        baseQuery = filter.SortOrder == SortOrder.Ascending
                            ? baseQuery.OrderBy(x => x.Id)
                            : baseQuery.OrderByDescending(x => x.Id);
                        break;

                    case nameof(MentorEntity.Profile.ProfileLastName):
                        baseQuery = filter.SortOrder == SortOrder.Ascending
                            ? baseQuery.OrderBy(x => x.Profile.ProfileLastName)
                            : baseQuery.OrderByDescending(x => x.Profile.ProfileLastName);
                        break;

                    default:
                        baseQuery = filter.SortOrder == SortOrder.Ascending
                            ? baseQuery.OrderBy(x => x.Profile.ProfileFirstName)
                            : baseQuery.OrderByDescending(x => x.Profile.ProfileFirstName);
                        break;
                }
            }

            if (!string.IsNullOrWhiteSpace(filter.SearchText))
            {
                baseQuery = baseQuery.Where(x =>
                    x.Profile.ProfileFirstName.Contains(filter.SearchText) ||
                    x.Profile.ProfileLastName.Contains(filter.SearchText)); 
            }

            var mentors = await baseQuery
                .Include(x => x.Profile).ThenInclude(x => x.Photo)
                .Include(m => m.Professions)
                .Skip(filter.Skip)
                .Take(filter.Take)
                .ToListAsync();

            _logger.LogInformation("Getting a list of filter mentors in the repository has finished successfuly");

            return mentors;
        }
        public async Task<double> GetMentorRatingSum(int mentorId)
        {
            var ratingSum = await _dbContext.Reviews.Where(x => x.ToProfileId == mentorId).SumAsync(x => x.Rating);
            return ratingSum;
        }
        public async Task<Dictionary<int, double>> GetMentorRatingSum(int[] mentorIds)
        {
            var ratingSums = await _dbContext
                .Reviews
                .Where(x => mentorIds.Contains(x.ToProfileId))
                .GroupBy(x => x.ToProfileId)
                .Select(x => new
                {
                    MentorId = x.Key,
                    RatingSum = x.Sum(x => x.Rating)
                })
                .ToDictionaryAsync(key => key.MentorId, value => value.RatingSum);

            return ratingSums;
        }

        public async Task<int> GetMentorTotalReviews(int mentorId)
        {
            var totalReviews = await _dbContext.Reviews.Where(x => x.ToProfileId == mentorId).CountAsync();
            return totalReviews;
        }

        public async Task<Dictionary<int, int>> GetMentorTotalReviews(int[] mentorIds) 
        {
            var totalReviews = await _dbContext.Reviews
                .Where(x => mentorIds.Contains(x.ToProfileId)) 
                .GroupBy(x => x.ToProfileId)
                .Select(x => new
                {
                    MentorId = x.Key,
                    TotalReviews = x.Count()
                })
                .ToDictionaryAsync(key => key.MentorId, value => value.TotalReviews);
            return totalReviews;
        }
        public async Task<List<MentorEntity>> GetAllMentorProfiles()
        {
            var listMentorEntity = await _dbContext.Mentors.ToListAsync();
            return listMentorEntity;
        }
    }



    
}
