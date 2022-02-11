using Store.Shared.Core.DomainObjects;
using System;

namespace Store.Customers.Domain.Entities
{
    public class Address : Entity
    {
        // EF relation
        protected Address() { }

        public Address(string street, string number, string complement, string neighborhood, string zipCode, string city, string state)
        {
            Street = street;
            Number = number;
            Complement = complement;
            Neighborhood = neighborhood;
            ZipCode = zipCode;
            City = city;
            State = state;
        }

        public string Street { get; private set; }
        public string Number { get; private set; }
        public string Complement { get; private set; }
        public string Neighborhood { get; private set; }
        public string ZipCode { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public Guid CustomerId { get; private set; }

        /// EF relation
        public Customer Customer { get; private set; }
    }
}
