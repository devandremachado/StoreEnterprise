using FluentValidation.Results;
using Store.Shared.Core.Messages;
using System.Threading.Tasks;

namespace Store.Shared.Core.Mediator
{
    public interface IMediatorHandler
    {
        Task PublishEvent<T>(T @event) where T : Event;
        Task<ValidationResult> SendCommand<T>(T command) where T : Command;
    }
}
