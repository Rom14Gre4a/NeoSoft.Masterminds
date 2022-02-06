using AutoMapper;
using NeoSoft.Masterminds.Domain.Models.Filters;
using NeoSoft.Masterminds.Models.Incoming.Filters;

namespace NeoSoft.Masterminds.MapConfig
{
    public class MentorFilterMap : Profile
    {
        public MentorFilterMap()
        {
            CreateMap<MentorFilterApiModel, MentorSearchFilter>();
        }
    }
}
