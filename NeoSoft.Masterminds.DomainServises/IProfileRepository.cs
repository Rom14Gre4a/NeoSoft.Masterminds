using NeoSoft.Masterminds.Domain.Models.Entities;
using System.Threading.Tasks;

namespace NeoSoft.Masterminds.Domain.Interfaces
{
    public interface IProfileRepository
    {
        Task<ProfileEntity> GetProfileById(int id);
    }
}
