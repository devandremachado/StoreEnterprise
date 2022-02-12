using FluentValidation;
using Store.Shared.Core.Messages;
using System;
using ValueObject = Store.Shared.Core.ValueObjects;

namespace Store.Customers.Application.Application
{
    public class CreateCustomerCommand : Command
    {
        public CreateCustomerCommand(Guid id, string name, string email, string cPF)
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

        public override bool IsValid()
        {
            ValidationResult = new CreateCustomerValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public class CreateCustomerValidation : AbstractValidator<CreateCustomerCommand>
        {
            public CreateCustomerValidation()
            {
                RuleFor(x => x.Id)
                    .NotEqual(Guid.Empty)
                    .WithMessage("Invalid Customer ID.");

                RuleFor(x => x.Name)
                    .NotEmpty()
                    .WithMessage("The Customer Name cannot be empty.");

                RuleFor(x => x.CPF)
                    .Must(ValidateCPF)
                    .WithMessage("Invalid Customer CPF");

                RuleFor(x => x.Email)
                    .Must(ValidateEmail)
                    .WithMessage("Invalud Customer Email.");
            }

            protected static bool ValidateCPF(string cpf)
            {
                return ValueObject.CPF.Validate(cpf);
            }

            protected static bool ValidateEmail(string email)
            {
                return ValueObject.Email.Validate(email);
            }
        }

    }
}
