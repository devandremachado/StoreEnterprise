using FluentValidation.Results;
using MediatR;
using Store.Shared.Core.Messages;
using System.Threading.Tasks;

namespace Store.Shared.Core.Mediator
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public MediatorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task PublishEvent<T>(T @event) where T : Event
        {
            await _mediator.Publish(@event);
        }

        public Task<ValidationResult> SendCommand<T>(T command) where T : Command
        {
            return _mediator.Send(command);
        }
    }
}
