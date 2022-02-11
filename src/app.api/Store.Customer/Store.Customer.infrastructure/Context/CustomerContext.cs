using Microsoft.EntityFrameworkCore;
using Store.Customers.Domain.Entities;
using Store.Shared.Core.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Customers.Infrastructure.Context
{
    public sealed class CustomerContext : DbContext, IUnitOfWork
    {
        public CustomerContext(DbContextOptions<CustomerContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Address> Address { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties()
                .Where(p => p.ClrType == typeof(string))))
            {
                property.SetIsUnicode(false); // varchar
                property.SetMaxLength(100);
            }

            foreach (var relation in modelBuilder.Model.GetEntityTypes().SelectMany(x => x.GetForeignKeys()))
            {
                relation.DeleteBehavior = DeleteBehavior.ClientSetNull;
            }

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CustomerContext).Assembly);
        }

        public async Task<bool> Commit()
        {
            return await base.SaveChangesAsync() > 0;
        }
    }
}
