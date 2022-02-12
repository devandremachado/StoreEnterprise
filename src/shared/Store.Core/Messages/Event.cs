using MediatR;
using System;

namespace Store.Shared.Core.Messages
{
    public class Event : Message, INotification
    {
        public Event()
        {
            Timestamp = DateTime.UtcNow;
        }

        public DateTime Timestamp { get; private set; }
    }
}
