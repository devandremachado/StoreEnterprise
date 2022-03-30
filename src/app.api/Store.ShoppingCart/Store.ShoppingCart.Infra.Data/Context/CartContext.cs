using Microsoft.EntityFrameworkCore;
using Store.Cart.Domain.Entities;
using System.Linq;

namespace Store.Cart.Infra.Data.Context
{
    public class CartContext : DbContext
    {
        public CartContext(DbContextOptions<CartContext> options) : base(options) 
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public DbSet<CartItem> CartItem { get; set; }
        public DbSet<CartCustomer> CartCustomer { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties()
                .Where(p => p.ClrType == typeof(string))))
            {
                property.SetIsUnicode(false);
                property.SetMaxLength(100);
            }

            modelBuilder.Entity<CartCustomer>()
                .HasIndex(c => c.CustomerId);

            modelBuilder.Entity<CartCustomer>()
                .HasMany(x => x.Items)
                .WithOne(x => x.CartCustomer)
                .HasForeignKey(x => x.CartId);

            foreach (var relation in modelBuilder.Model.GetEntityTypes().SelectMany(x => x.GetForeignKeys()))
            {
                relation.DeleteBehavior = DeleteBehavior.ClientSetNull;
            }
        }
    }
}
