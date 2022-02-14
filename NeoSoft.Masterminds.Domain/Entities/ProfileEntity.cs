using NeoSoft.Masterminds.Domain.Models.Entities.Identity;
using System.Collections.Generic;

namespace NeoSoft.Masterminds.Domain.Models.Entities
{
    public class ProfileEntity : BaseEntity
    {
        public int? PhotoId { get; set; }
        public FileEntity Photo { get; set; }

        public MentorEntity Mentor { get; set; }
        public List<MentorEntity> Favorites { get; set; } 

        public string ProfileFirstName { get; set; }
        public string ProfileLastName { get; set; }
        public AppUser AppUser { get; set; }
        public virtual IList<ReviewEntity> SentReviews { get; set; }
        public virtual IList<ReviewEntity> RecivedReviews { get; set; }     
    }
}
