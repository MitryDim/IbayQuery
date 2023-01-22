using Dal.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dal
{
    public class DatabaseContext:DbContext
    {
        public DbSet<UsersEntities> Users { get; set; }
        public DbSet<Products> Products { get; set; }


        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) 
        {
        }


        

    }
}
