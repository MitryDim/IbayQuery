using Dal.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dal
{
    public class DatabaseContext:DbContext
    {
        public DbSet<UsersEntities> Users { get; set; }
        public DbSet<ProductsEntities> Products { get; set; }
        public DbSet<OrdersEntities> Orders { get; set; }

        public DbSet<CartsEntities> Carts { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) 
        {
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<ProductsEntities>()
        //        .HasOne(p => p.User)
        //        .WithMany()
        //        .HasForeignKey(p => p.OwnerId);
        //}

    }
}
