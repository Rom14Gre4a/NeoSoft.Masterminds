using NeoSoft.Masterminds.Domain;
using NeoSoft.Masterminds.Domain.Models;
using NeoSoft.Masterminds.Domain.Models.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NeoSoft.Masterminds.Services.Interfaces
{
    public interface IMentorService
    {
        Task<MentorModel>  GetMentorProfileById(int mentorId);
        Task<List<MentorListModel>> Get(int skip = 0, int take = 15);

    }
}
