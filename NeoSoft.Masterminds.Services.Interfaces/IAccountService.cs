using NeoSoft.Masterminds.Domain.Models.Models.Auth;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NeoSoft.Masterminds.Services.Interfaces
{
    public interface IAccountService
    {
        Task<TokenModel> Login(Login login);

        Task<TokenModel> CreateNewUserAccount(UserRegistration registration);

        Task<TokenModel> CreateNewMentorAccount(MentorRegistration registration);
    }
}
