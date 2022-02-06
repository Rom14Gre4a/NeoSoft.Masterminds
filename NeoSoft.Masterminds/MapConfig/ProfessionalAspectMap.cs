using AutoMapper;
using NeoSoft.Masterminds.Domain.Models.Models;
using NeoSoft.Masterminds.Models.Outcoming;

namespace NeoSoft.Masterminds.MapConfig
{
    public class ProfessionalAspectMap : Profile
    {
        public ProfessionalAspectMap()
        {
            CreateMap<ProfessionalAspectModel, ProfessionalAspectViewModel>();
        }
    }
}
