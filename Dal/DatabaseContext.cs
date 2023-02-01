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

        public DbSet<CartsItemsEntities> CartsItems { get; set; }

        public DbSet<PayementsEntities> Payements { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) 
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CartsEntities>()
                .HasOne<UsersEntities>()
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(c => c.UserId);

                

            modelBuilder.Entity<ProductsEntities>()
               .HasOne<UsersEntities>()
               .WithMany()
               .OnDelete(DeleteBehavior.NoAction)
               .HasForeignKey(c => c.OwnedId);


            modelBuilder.Entity<CartsItemsEntities>()
                 .HasOne(ci => ci.Cart)
                 .WithMany(c => c.CartItems)
                 .HasForeignKey(ci => ci.CartId);




        }

    }
}
