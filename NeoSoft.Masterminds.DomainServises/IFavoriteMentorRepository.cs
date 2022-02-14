using NeoSoft.Masterminds.Domain.Models.Entities;
using NeoSoft.Masterminds.Domain.Models.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NeoSoft.Masterminds.Domain.Interfaces
{
    public interface IFavoriteMentorRepository
    {
        Task<List<MentorEntity>> GetFavoriteMentorsAsync(int profileId);
        Task<AppUser> GetFavoriteUser(string email);
        Task<List<MentorEntity>> Update(int mentorId);
        Task<int> GetProfileTotalFavorites(int mentorId);
    }
}
