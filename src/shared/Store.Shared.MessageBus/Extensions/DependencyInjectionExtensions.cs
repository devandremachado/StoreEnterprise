using Microsoft.Extensions.DependencyInjection;
using Store.Shared.MessageBus.Interfaces;
using System;

namespace Store.Shared.MessageBus.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddMessageBus(this IServiceCollection service, string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException();

            service.AddSingleton<IMessageBus>(new MessageBus(connectionString));

            return service;
        }
    }
}
