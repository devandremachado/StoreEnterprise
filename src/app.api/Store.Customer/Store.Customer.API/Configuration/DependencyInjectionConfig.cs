using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Store.Customers.Domain.Commands;
using Store.Customers.Domain.Events;
using Store.Customers.Domain.Handlers.Commands;
using Store.Customers.Domain.Handlers.Events;
using Store.Customers.Domain.Repositories;
using Store.Customers.Infra.Data.Context;
using Store.Customers.Infra.Data.Repositories;
using Store.Shared.Core.Mediator;

namespace Store.Customers.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<IRequestHandler<CreateCustomerCommand, ValidationResult>, CustomerCommandHandler>();

            services.AddScoped<INotificationHandler<CreateCustomerEvent>, CustomerEventHandler>();

            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<CustomerContext>();
        }
    }
}
