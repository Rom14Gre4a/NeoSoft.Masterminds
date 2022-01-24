using Microsoft.AspNetCore.Identity;

namespace NeoSoft.Masterminds.Domain.Models.Entities.Identity
{
    public class AppRole : IdentityRole<int>
    {
        public AppUser User { get; set; }
        public AppUser Mentor { get; set; }
    }
}
