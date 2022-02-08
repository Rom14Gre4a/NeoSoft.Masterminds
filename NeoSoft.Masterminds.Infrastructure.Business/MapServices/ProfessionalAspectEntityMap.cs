using AutoMapper;
using NeoSoft.Masterminds.Domain.Models.Entities;
using NeoSoft.Masterminds.Domain.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NeoSoft.Masterminds.Infrastructure.Business.MapServices
{
    public class ProfessionalAspectEntityMap : Profile 
    {
        public ProfessionalAspectEntityMap()
        {
            CreateMap<ProfessionalAspectEntity, ProfessionalAspectModel>();
        }
    }
}
