﻿using FluentValidation.Results;
using MediatR;
using Store.Customers.Application.Application;
using Store.Customers.Application.CQRS.Events;
using Store.Customers.Domain.Entities;
using Store.Customers.Domain.Repositories;
using Store.Shared.Core.Messages;
using System.Threading;
using System.Threading.Tasks;

namespace Store.Customers.Application.CQRS.Commands.Handlers
{
    public class CustomerCommandHandler : CommandHandler,
                                          IRequestHandler<CreateCustomerCommand, ValidationResult>
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<ValidationResult> Handle(CreateCustomerCommand command, CancellationToken cancellationToken)
        {
            if (command.IsValid() == false)
                return command.ValidationResult;

            var customer = new Customer(command.Id, command.Name, command.Email, command.CPF);

            var customerRepository = await _customerRepository.GetByCPF(customer.CPF.Number);
            if (customerRepository != null)
            {
                AddErrorMessage("Customer.CPF", "this CPF is already in use.");
                return ValidationResult;
            }

            _customerRepository.Add(customer);

            customer.AddNotificationEvent(new CreateCustomerEvent(command.Id, command.Name, command.Email, command.CPF));

            return await SaveChanges(_customerRepository.UnitOfWork);
        }
    }
}
