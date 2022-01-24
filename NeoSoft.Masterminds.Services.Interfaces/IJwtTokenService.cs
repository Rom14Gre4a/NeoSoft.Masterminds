using NeoSoft.Masterminds.Domain.Models.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace NeoSoft.Masterminds.Services.Interfaces
{
    public interface IJwtTokenService
    {
        string CreateAccessToken(AppUser appUser, IList<string> appUserRoles = null);
    }
}
