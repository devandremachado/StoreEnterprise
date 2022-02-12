using FluentValidation.Results;
using MediatR;
using System;

namespace Store.Shared.Core.Messages
{
    public abstract class Command : Message, IRequest<ValidationResult>
    {
        public Command()
        {
            Timestamp = DateTime.UtcNow;
        }

        public DateTime Timestamp { get; private set; }
        public ValidationResult ValidationResult { get; set; }

        public virtual bool IsValid()
        {
            throw new NotImplementedException();
        }

    }
}
