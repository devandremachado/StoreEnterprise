using Store.Customers.Domain.Entities;
using Store.Shared.Core.Data;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.Customers.Domain.Repositories
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        void Add(Customer customer);
        Task<IEnumerable<Customer>> GetAll();
        Task<Customer> GetByCPF(string cpf);
    }
}
