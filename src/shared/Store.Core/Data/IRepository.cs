using Store.Shared.DomainObjects;
using System;

namespace Store.Shared.Data
{
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
    }
}
