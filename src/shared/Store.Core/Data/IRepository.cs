using Store.Shared.Core.DomainObjects;
using System;

namespace Store.Shared.Core.Data
{
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
    }
}
