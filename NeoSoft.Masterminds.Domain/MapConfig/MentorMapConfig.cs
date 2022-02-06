using AutoMapper;
using NeoSoft.Masterminds.Domain.Models.Entities;

namespace NeoSoft.Masterminds.Domain.Models.MapConfig
{
    public class MentorMapConfig : Profile
    {
        public MentorMapConfig()
        {
            CreateMap<MentorEntity,MentorModel>();
        }
    }
}
