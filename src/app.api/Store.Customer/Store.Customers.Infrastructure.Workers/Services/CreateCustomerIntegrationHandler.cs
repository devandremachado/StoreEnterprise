using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Store.Customers.Domain.Entities.Commands;
using Store.Shared.Core.Mediator;
using Store.Shared.Core.Messages.Integration.Events.Request;
using Store.Shared.Core.Messages.Integration.Events.Response;
using Store.Shared.MessageBus.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Store.Customers.Infrastructure.Workers.Services
{
    public class CreateCustomerIntegrationHandler : BackgroundService
    {
        private readonly IMessageBus _bus;
        private readonly IServiceProvider _serviceProvider;

        public CreateCustomerIntegrationHandler(IServiceProvider serviceProvider,
                                                IMessageBus bus)
        {
            _serviceProvider = serviceProvider;
            _bus = bus;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _bus.RespondAsync<CreateUserIntegrationEvent, ResponseMessage>(async request => 
                await CreateCustomer(request));

            return Task.CompletedTask;
        }

        private async Task<ResponseMessage> CreateCustomer(CreateUserIntegrationEvent request)
        {
            var customerCommand = new CreateCustomerCommand(request.Id, request.Name, request.Email, request.CPF);

            ValidationResult result;
            using (var scope = _serviceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
                result = await mediator.SendCommand(customerCommand);
            }

            return new ResponseMessage(result);
        }

    }
}

