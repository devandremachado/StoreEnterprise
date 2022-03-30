using Store.Shared.Core.Messages;
using System;

namespace Store.Customers.Domain.Events
{
    public class CreateCustomerEvent : Event
    {
        public CreateCustomerEvent(Guid id, string name, string email, string cpf)
        {
            AggregateId = id;
            Id = id;
            Name = name;
            Email = email;
            CPF = cpf;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string CPF { get; private set; }
    }
}
