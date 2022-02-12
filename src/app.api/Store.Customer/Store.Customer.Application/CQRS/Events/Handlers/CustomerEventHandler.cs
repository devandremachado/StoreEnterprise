using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Store.Customers.Application.CQRS.Events.Handlers
{
    public class CustomerEventHandler : INotificationHandler<CreateCustomerEvent>
    {
        public Task Handle(CreateCustomerEvent notification, CancellationToken cancellationToken)
        {
            // Enviar email de confirmação..

            return Task.CompletedTask;
        }
    }
}
