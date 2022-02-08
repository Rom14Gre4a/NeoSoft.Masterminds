using AutoMapper;
using NeoSoft.Masterminds.Domain.Models.Models.Auth;
using NeoSoft.Masterminds.Models.Outcoming;

namespace NeoSoft.Masterminds.MapConfig
{
    public class TokenMap : Profile
    {
        public TokenMap()
        {
            CreateMap<TokenModel, TokenApiModel>();
        }
    }
}
