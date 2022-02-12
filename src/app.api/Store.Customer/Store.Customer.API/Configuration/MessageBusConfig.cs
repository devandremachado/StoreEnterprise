﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Store.Customers.Infrastructure.Workers.Services;
using Store.Shared.Core.Utils.Extensions;
using Store.Shared.MessageBus.Extensions;

namespace Store.Customers.API.Configuration
{
    public static class MessageBusConfig
    {
        public static void AddMessageBusConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMessageBus(configuration.GetMessageQueueConnection("MessageBus"))
                .AddHostedService<CreateCustomerIntegrationHandler>();
        }
    }
}
