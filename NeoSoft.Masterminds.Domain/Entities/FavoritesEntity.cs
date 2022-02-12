using System;
using System.Collections.Generic;
using System.Text;

namespace NeoSoft.Masterminds.Domain.Models.Entities
{
    public class FavoritesEntity : BaseEntity
    {
        public int MentorId { get; set; }
        public MentorEntity Mentor { get; set; }

        public int ProfileId { get; set; }
        public ProfileEntity Profile { get; set; }
    }
}
