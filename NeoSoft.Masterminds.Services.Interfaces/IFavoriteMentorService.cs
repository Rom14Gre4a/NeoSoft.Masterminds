using NeoSoft.Masterminds.Domain;
using NeoSoft.Masterminds.Domain.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NeoSoft.Masterminds.Services.Interfaces
{
    public interface IFavoriteMentorService
    {
        Task<List<MentorListModel>> GetAll( string email);
        //Task<MentorModel> AddToFavorites();
      //  Task<MentorModel> RemoveFromFavorites();
    }
}
