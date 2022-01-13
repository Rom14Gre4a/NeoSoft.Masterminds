using NeoSoft.Masterminds.Domain;
using System;
using System.Threading.Tasks;

namespace NeoSoft.Masterminds.Services.Interfaces
{
    public interface IMentorService
    {
        Task<MentorModel>  GetMentorProfileById(int mentorId);

    }
}
