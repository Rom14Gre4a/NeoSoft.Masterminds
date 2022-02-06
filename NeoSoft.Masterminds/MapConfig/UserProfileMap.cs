using AutoMapper;
using NeoSoft.Masterminds.Domain.Models.Models;
using NeoSoft.Masterminds.Models.Outcoming;

namespace NeoSoft.Masterminds.MapConfig
{
    public class UserProfileMap : Profile
    {
        public UserProfileMap()
        {
            CreateMap<UserProfileModel, UserProfileApiModel>();
        }
    }
}
