using NeoSoft.Masterminds.Domain.Models.Entities.Identity;
using NeoSoft.Masterminds.Domain.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NeoSoft.Masterminds.Services.Interfaces
{
    public interface IUserProfileService
    {
        Task<UserProfileModel> GetProfileByEmail(string email);
    }
}
