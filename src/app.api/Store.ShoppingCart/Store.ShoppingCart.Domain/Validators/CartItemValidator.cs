using FluentValidation;
using Store.Cart.Domain.Entities;
using System;

namespace Store.Cart.Domain.Validators
{
    public class CartItemValidator : AbstractValidator<CartItem>
    {
        public CartItemValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEqual(Guid.Empty)
                .WithMessage("Invalid Product Id");

            RuleFor(x => x.Name)
                .NotEmpty();

            RuleFor(x => x.Quantity)
                .GreaterThan(0);

            RuleFor(x => x.Quantity)
                .LessThan(5);

            RuleFor(x => x.Amount)
                .GreaterThan(0);
        }
    }
}
