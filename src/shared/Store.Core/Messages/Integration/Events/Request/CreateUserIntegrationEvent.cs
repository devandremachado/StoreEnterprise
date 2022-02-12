using Store.Shared.Core.Messages.Integration.Contracts;
using System;

namespace Store.Shared.Core.Messages.Integration.Events.Request
{
    public class CreateUserIntegrationEvent : IntegrationEvent
    {
        public CreateUserIntegrationEvent(Guid id, string name, string email, string cpf)
        {
            Id = id;
            Name = name;
            Email = email;
            CPF = cpf;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string CPF { get; private set; }
    }
}
