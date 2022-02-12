using FluentValidation.Results;

namespace Store.Shared.Core.Messages.Integration.Events.Response
{
    public class ResponseMessage : Message
    {
        public ResponseMessage(ValidationResult validationResult)
        {
            ValidationResult = validationResult;
        }

        public ValidationResult ValidationResult { get; set; }
    }
}
