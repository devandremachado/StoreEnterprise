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

        private void CalculateTotalCartPrice()
        {
            Amount = Items.Sum(x => x.CalculatePrice());
        }

        public bool ProductAlreadyExistsInCart(CartItem item)
        {
            return Items.Any(x => x.ProductId == item.Id);
        }

        public CartItem GetProductById(Guid ProductId)
        {
            return Items.FirstOrDefault(x => x.ProductId == ProductId);
        }

        public void AddItem(CartItem item)
        {
            if (item.IsValid() == false) return;

            item.LinkToCart(Id);

            if (ProductAlreadyExistsInCart(item))
            {
                var itemExists = GetProductById(item.ProductId);
                itemExists.AddQuantity(item.Quantity);

                item = itemExists;
                Items.Remove(itemExists);
            }

            Items.Add(item);
            CalculateTotalCartPrice();
        }
    }
}
