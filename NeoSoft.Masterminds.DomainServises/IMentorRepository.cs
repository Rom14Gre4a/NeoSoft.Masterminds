using NeoSoft.Masterminds.Domain.Models;
using NeoSoft.Masterminds.Domain.Models.Entities;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NeoSoft.Masterminds.Domain.Interfaces
{
    public interface IMentorRepository
    {
        Task<MentorEntity> GetMentorProfileById(int mentorId);

        Task<List<MentorEntity>> Get(GetFilter filter);
    }
}
