using AutoMapper;
using NeoSoft.Masterminds.Domain.Models.Models.Auth;
using NeoSoft.Masterminds.Models.Incoming;

namespace NeoSoft.Masterminds.MapConfig
{
    public class LoginMap : Profile
    {
        public LoginMap()
        {
            CreateMap<Login, IncomLogin>();
        }
    }
}
