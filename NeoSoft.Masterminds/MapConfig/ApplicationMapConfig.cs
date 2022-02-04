using AutoMapper;
using NeoSoft.Masterminds.Models;

namespace NeoSoft.Masterminds.Domain.Models.MapConfig
{
    public class ApplicationMapConfig : Profile
    {
        public ApplicationMapConfig()
        {
            CreateMap<MentorModel, MentorView>();
        }
    }
}
