using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Store.Customers.Application.Application;
using Store.Customers.Application.CQRS.Commands.Handlers;
using Store.Customers.Application.CQRS.Events;
using Store.Customers.Application.CQRS.Events.Handlers;
using Store.Customers.Domain.Repositories;
using Store.Customers.Infrastructure.Context;
using Store.Customers.Infrastructure.Repositories;
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
