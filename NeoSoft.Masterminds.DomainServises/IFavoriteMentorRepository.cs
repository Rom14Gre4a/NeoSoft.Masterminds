using NeoSoft.Masterminds.Domain.Models.Entities;
using NeoSoft.Masterminds.Domain.Models.Entities.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NeoSoft.Masterminds.Domain.Interfaces
{
    public interface IFavoriteMentorRepository
    {
        Task<AppUser> GetFavoriteUser(string email);
        Task<List<MentorEntity>> GetFavoriteMentorsAsync(int profileId);
        Task<ProfileEntity> GetFavoriteMentorAsync(int profileId);
        Task<ProfileEntity> GetFavoriteProfile(string email);
        Task AddFavorite(ProfileEntity profile, int mentorId);
        Task RemoveFavorite(ProfileEntity profile, int mentorId);
        Task<int> GetProfileTotalFavorites(int mentorId);



    }
}
