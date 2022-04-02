using FluentValidation.Results;
using Store.Cart.Domain.Validators;
using System;
using System.Collections.Generic;
using System.Linq;

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
        public ValidationResult ValidationResult { get; private set; }


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

        public void UpdateItem(CartItem item)
        {
            var itemExists = GetProductById(item.ProductId);

            Items.Remove(itemExists);
            Items.Add(item);

            CalculateTotalCartPrice();
        }

        public void RemoveItem(CartItem item)
        {
            var itemExists = GetProductById(item.ProductId);

            if (itemExists is null) 
                throw new Exception("The product doesn't belong to the order");

            Items.Remove(itemExists);

            CalculateTotalCartPrice();
        }

        public void UpdateItemQuantity(CartItem item, int quantity)
        {
            item.UpdateQuantity(quantity);
            UpdateItem(item);
        }

        public bool IsValid()
        {
            var errors = Items.SelectMany(item => new CartItemValidator().Validate(item).Errors).ToList();
            errors.AddRange(new CartCustomerValidator().Validate(this).Errors);
            ValidationResult = new ValidationResult(errors);

            return ValidationResult.IsValid;
        }

    }
}
