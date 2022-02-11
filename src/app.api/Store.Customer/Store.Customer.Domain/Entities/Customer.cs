using Store.Shared.Core.DomainObjects;
using Store.Shared.Core.ValueObjects;
using System;

namespace Store.Customers.Domain.Entities
{
    public class Customer : Entity, IAggregateRoot
    {
        //EF relation
        protected Customer()
        { }

        public Customer(Guid id, string name, string email, string cpf)
        {
            Id = id;
            Name = name;
            Email = new Email(email);
            CPF = new CPF(cpf);
            Active = true;
        }

        public string Name { get; private set; }
        public Email Email { get; private set; }
        public CPF CPF { get; private set; }
        public bool Active { get; private set; }
        public Address Address { get; private set; }

        public void SetEmail(string email)
        {
            Email = new Email(email);
        }

        public void SetAddress(Address address)
        {
            Address = address;
        }
    }
}
