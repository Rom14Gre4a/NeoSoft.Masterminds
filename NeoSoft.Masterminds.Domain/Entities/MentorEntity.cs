using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NeoSoft.Masterminds.Domain.Models.Entities
{
    public class MentorEntity : BaseEntity
    {
       
        public decimal HourlyRate { get; set; }
        public string Description { get; set; }

        public ProfileEntity Profile { get; set; }
        public List<FavoritesEntity> Favorites { get; set; } = new List<FavoritesEntity>();

        public IList<ProfessionalAspectEntity> ProfessionalAspects { get; set; }
        public IList<ProfessionEntity> Professions { get; set; }
    }
}
