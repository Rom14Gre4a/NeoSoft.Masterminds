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
        Task<double> GetMentorRatingSum(int mentorId);

        Task<Dictionary<int, double>> GetMentorRatingSum(int[] mentorIds);

        Task<int> GetMentorTotalReviews(int mentorId);

        Task<Dictionary<int, int>> GetMentorTotalReviews(int[] mentorIds);


    }
}
