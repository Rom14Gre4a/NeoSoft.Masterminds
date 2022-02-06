using AutoMapper;
using NeoSoft.Masterminds.Domain.Models.Models;
using NeoSoft.Masterminds.Models.Outcoming;

namespace NeoSoft.Masterminds.MapConfig
{
    public class ProfessionMap : Profile
    {
        public ProfessionMap()
        {
            CreateMap<ProfessionsModel, ProfessionViewModel>();
        }
    }
}
