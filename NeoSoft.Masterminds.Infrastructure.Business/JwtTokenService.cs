using Microsoft.Extensions.Options;
using NeoSoft.Masterminds.Domain.Models.Entities.Identity;
using NeoSoft.Masterminds.Domain.Models.Options;
using NeoSoft.Masterminds.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace NeoSoft.Masterminds.Infrastructure.Business
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly JwtTokenOptions _tokenOptions;
        private string jwtToken;

        public JwtTokenService(IOptions<JwtTokenOptions> tokenOptions)
        {
            _tokenOptions = tokenOptions.Value;
        }
        public string CreateAccessToken(AppUser appUser, IList<string> appUserRoles = null)
        {
            return jwtToken;
        }
    }
}
