using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Store.Catalog.Domain.Entities;
using Store.Shared.Core.Data;
using Store.Shared.Core.Messages;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Catalog.Infra.Data.Context
{
    public class CatalogContext : DbContext, IUnitOfWork
    {
        public CatalogContext(DbContextOptions<CatalogContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<ValidationResult>();
            modelBuilder.Ignore<Event>();

            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties()
                .Where(p => p.ClrType == typeof(string))))
            {
                property.SetIsUnicode(false); // varchar
                property.SetMaxLength(100);
            }

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogContext).Assembly);
        }

        public async Task<bool> Commit()
        {
            return await base.SaveChangesAsync() > 0;
        }
    }
}
