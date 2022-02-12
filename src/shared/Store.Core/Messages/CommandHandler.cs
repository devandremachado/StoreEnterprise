using FluentValidation.Results;
using Store.Shared.Core.Data;
using System.Threading.Tasks;

namespace Store.Shared.Core.Messages
{
    public abstract class CommandHandler
    {
        protected ValidationResult ValidationResult;

        protected CommandHandler()
        {
            ValidationResult = new ValidationResult();
        }

        protected void AddErrorMessage(string message)
        {
            ValidationResult.Errors.Add(new ValidationFailure(string.Empty, message));
        }

        protected void AddErrorMessage(string title, string message)
        {
            ValidationResult.Errors.Add(new ValidationFailure(title, message));
        }

        protected async Task<ValidationResult> SaveChanges(IUnitOfWork unitOfWork)
        {
            if(await unitOfWork.Commit() == false)
                AddErrorMessage("An unexpected error occurred.");

            return ValidationResult;
        }
    }
}
