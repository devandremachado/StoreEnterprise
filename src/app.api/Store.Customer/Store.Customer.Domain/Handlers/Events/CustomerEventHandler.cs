using MediatR;
using Store.Customers.Domain.Events;
using System.Threading;
using System.Threading.Tasks;

namespace Store.Customers.Domain.Handlers.Events
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
