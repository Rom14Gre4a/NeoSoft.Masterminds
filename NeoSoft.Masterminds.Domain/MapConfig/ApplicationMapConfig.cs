using AutoMapper;
using NeoSoft.Masterminds.Domain.Models.Entities;

namespace NeoSoft.Masterminds.Domain.Models.MapConfig
{
    public class ApplicationMapConfig : Profile
    {
        public ApplicationMapConfig()
        {
            CreateMap<MentorEntity,MentorModel>();
        }
    }
}
