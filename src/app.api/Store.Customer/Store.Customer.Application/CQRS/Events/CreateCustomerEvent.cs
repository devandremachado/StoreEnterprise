using Store.Shared.Core.Messages;
using System;

namespace Store.Customers.Application.CQRS.Events
{
    public class CreateCustomerEvent : Event
    {
        public CreateCustomerEvent(Guid id, string name, string email, string cPF)
        {
            AggregateId = id;
            Id = id;
            Name = name;
            Email = email;
            CPF = cPF;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string CPF { get; private set; }
    }
}
