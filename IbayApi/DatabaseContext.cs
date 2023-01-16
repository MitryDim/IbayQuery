using Dal;
using Microsoft.EntityFrameworkCore;

namespace IbayApi
{
    public partial class DatabaseContext:DbContext
    {
        public DbSet<Users> Users { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) 
        {
        }
        

    }
}
