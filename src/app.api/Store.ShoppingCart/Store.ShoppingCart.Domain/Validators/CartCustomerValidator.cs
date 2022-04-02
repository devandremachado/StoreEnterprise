using FluentValidation;
using Store.Cart.Domain.Entities;
using System;

namespace Store.Cart.Domain.Validators
{
    public class CartCustomerValidator : AbstractValidator<CartCustomer>
    {
        public CartCustomerValidator()
        {
            RuleFor(x => x.CustomerId)
                .NotEqual(Guid.Empty)
                .WithMessage("Invalid Customer Id");

            RuleFor(x => x.Items.Count)
                .GreaterThan(0);

            RuleFor(x => x.Amount)
                .GreaterThan(0);
        }
    }
}
