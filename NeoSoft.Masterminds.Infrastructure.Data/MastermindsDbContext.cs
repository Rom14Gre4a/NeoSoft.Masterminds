
using Microsoft.EntityFrameworkCore;


namespace NeoSoft.Masterminds.Infrastructure.Data
{
    public class MastermindsDbContext : DbContext
    {
        public MastermindsDbContext(DbContextOptions<MastermindsDbContext> options)
           : base(options)
        {
            Database.EnsureCreated();
        }

    }
}
