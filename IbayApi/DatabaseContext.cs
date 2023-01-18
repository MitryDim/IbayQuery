using Dal;
using Microsoft.EntityFrameworkCore;

namespace IbayApi
{
    public class DatabaseContext:DbContext
    {
        public DbSet<Users> Users { get; set; }
        public DbSet<Products> Products { get; set; }


        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) 
        {
        }


        

    }
}
