using AutoMapper;
using NeoSoft.Masterminds.Domain.Models.Filters;
using NeoSoft.Masterminds.Models.Incoming.Filters;

namespace NeoSoft.Masterminds.MapConfig
{
    public class ProfFilter : Profile
    {
        public ProfFilter()
        {
            CreateMap<ProfAspFilterApiModel, ProfessionalAspectSearchFilter>();

            CreateMap<ProfFilterApiModel, ProfessionFilter>();
        }
    }
}
