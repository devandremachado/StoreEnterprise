using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Store.Customers.Domain.Entities;
using Store.Shared.Core.Data;
using Store.Shared.Core.DomainObjects;
using Store.Shared.Core.Mediator;
using Store.Shared.Core.Messages;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Customers.Infrastructure.Context
{
    public sealed class CustomerContext : DbContext, IUnitOfWork
    {

        private readonly IMediatorHandler _mediatorHandler;

        public CustomerContext(DbContextOptions<CustomerContext> options, IMediatorHandler mediatorHandler) : base(options)
        {
            _mediatorHandler = mediatorHandler;

            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Address> Address { get; set; }

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

            foreach (var relation in modelBuilder.Model.GetEntityTypes().SelectMany(x => x.GetForeignKeys()))
            {
                relation.DeleteBehavior = DeleteBehavior.ClientSetNull;
            }

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CustomerContext).Assembly);
        }

        public async Task<bool> Commit()
        {
            var commitSuccessfully = await base.SaveChangesAsync() > 0;

            if (commitSuccessfully)
                await _mediatorHandler.PublishEvents(this);

            return commitSuccessfully;
        }
    }


    public static class MediatorExtension
    {
        public static async Task PublishEvents<T>(this IMediatorHandler mediator, T context) where T : DbContext
        {
            var domainEntites = context.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.Notifications != null && x.Entity.Notifications.Any());

            var domainEvents = domainEntites
                .SelectMany(x => x.Entity.Notifications)
                .ToList();

            domainEntites.ToList()
                .ForEach(x => x.Entity.ClearNotificationEvent());

            var tasks = domainEvents
                .Select(async (domainEvents) =>
                {
                    await mediator.PublishEvent(domainEvents);
                });

            await Task.WhenAll(tasks);
        }
    }
}
