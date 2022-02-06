using AutoMapper;
using NeoSoft.Masterminds.Domain.Models.Models;
using NeoSoft.Masterminds.Models.Outcoming;

namespace NeoSoft.Masterminds.MapConfig
{
    public class MentorListMap : Profile
    {
        public MentorListMap()
        {
            CreateMap<MentorListModel, MentorListView>();
        }
    }
}
