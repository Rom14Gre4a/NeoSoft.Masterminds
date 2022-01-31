using AutoMapper;
using NeoSoft.Masterminds.Domain;
using NeoSoft.Masterminds.Domain.Models.Entities;
using NeoSoft.Masterminds.Models;

namespace NeoSoft.Masterminds.MapConfig
{
    public class AppMapConfig : Profile
    {
        public AppMapConfig()
        {
            CreateMap<MentorEntity, MentorModel>();

            CreateMap<MentorModel, MentorView>();
        }
    }
}
