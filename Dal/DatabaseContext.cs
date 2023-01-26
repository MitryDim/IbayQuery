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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CartsEntities>()
                .HasOne(c => c.User)
                .WithMany(u => u.Carts)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<CartsEntities>()
                .HasOne(c => c.Product)
                .WithMany(p => p.Carts)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<OrdersEntities>()
                .HasOne(c => c.Cart)
                .WithMany(p => p.Orders)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ProductsEntities>()
                .HasOne(c => c.User)
                .WithMany(p => p.Products)
                .OnDelete(DeleteBehavior.NoAction);
        }

    }
}
