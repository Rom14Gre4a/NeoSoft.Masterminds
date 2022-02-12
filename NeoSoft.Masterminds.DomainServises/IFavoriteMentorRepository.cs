using NeoSoft.Masterminds.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NeoSoft.Masterminds.Domain.Interfaces
{
    public interface IFavoriteMentorRepository
    {
        Task<List<MentorEntity>> GetAll();
       // Task<List<MentorEntity>> Update();
    }
}
