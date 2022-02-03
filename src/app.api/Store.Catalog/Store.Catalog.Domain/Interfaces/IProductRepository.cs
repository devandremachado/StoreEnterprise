using Store.Catalog.Domain.Entities;
using Store.Shared.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.Catalog.Domain.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetAll();
        Task<Product> GetById(Guid id);

        void Insert(Product product);
        void Update(Product product);
    }
}
