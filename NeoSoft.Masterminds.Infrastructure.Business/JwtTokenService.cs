using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NeoSoft.Masterminds.Domain.Models.Entities.Identity;
using NeoSoft.Masterminds.Domain.Models.Options;
using NeoSoft.Masterminds.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NeoSoft.Masterminds.Infrastructure.Business
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly JwtTokenOptions _tokenOptions;
        

        public JwtTokenService(IOptions<JwtTokenOptions> tokenOptions)
        {
            _tokenOptions = tokenOptions.Value;
        }
        public string CreateAccessToken(AppUser appUser, IList<string> appUserRoles = null)
        {
            var identity = GetIdentity(appUser, appUserRoles);

            var currentDateTime = DateTime.UtcNow;

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _tokenOptions.Issuer,
                audience: _tokenOptions.Audience,
                notBefore: currentDateTime,
                claims: identity.Claims,
                expires: currentDateTime.Add(TimeSpan.FromMinutes(_tokenOptions.LifetimeMin)),
                signingCredentials: new SigningCredentials(GetSymmetricSecurityKey(_tokenOptions.SecurityKey), SecurityAlgorithms.HmacSha256));

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return jwtToken;
        }
        private ClaimsIdentity GetIdentity(AppUser appUser, IList<string> appUserRoles = null)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, appUser.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, string.Join(", ", appUserRoles))
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }

        private static SymmetricSecurityKey GetSymmetricSecurityKey(string securityKey)
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(securityKey));
        }
    }
}
