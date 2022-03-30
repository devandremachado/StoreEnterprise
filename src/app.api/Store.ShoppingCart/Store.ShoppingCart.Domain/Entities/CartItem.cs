using System;

namespace Store.Cart.Domain.Entities
{
    public class CartItem
    {
        public CartItem()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }
        public Guid ProductId { get; private set; }
        public string Name { get; private set; }
        public int Quantity { get; private set; }
        public decimal Amount { get; private set; }
        public string PathImage { get; private set; }

        public Guid CartId { get; private set; }
        public CartCustomer CartCustomer { get; private set; }
    }
}
