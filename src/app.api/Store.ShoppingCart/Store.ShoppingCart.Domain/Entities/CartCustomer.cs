using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Cart.Domain.Entities
{
    public class CartCustomer
    {
        public CartCustomer(Guid customerId)
        {
            Id = Guid.NewGuid();
            CustomerId = customerId;
        }

        //EF
        public CartCustomer() { }

        public Guid Id { get; private set; }
        public Guid CustomerId { get; private set; }
        public decimal Amount { get; private set; }
        public List<CartItem> Items { get; set; } = new List<CartItem>();
    }
}
