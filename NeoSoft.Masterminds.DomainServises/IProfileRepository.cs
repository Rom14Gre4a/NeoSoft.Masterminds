using NeoSoft.Masterminds.Domain.Models.Entities;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NeoSoft.Masterminds.Domain.Interfaces
{
    public interface IProfileRepository
    {
        Task<bool> AddProfile(ProfileEntity profile);
        Task<ProfileEntity> GetProfile(int profileId);
    }
}
