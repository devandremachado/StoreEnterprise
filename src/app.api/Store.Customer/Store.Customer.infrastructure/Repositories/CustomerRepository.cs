using Microsoft.EntityFrameworkCore;
using Store.Customers.Domain.Entities;
using Store.Customers.Domain.Repositories;
using Store.Customers.Infra.Data.Context;
using Store.Shared.Core.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.Customers.Infra.Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerContext _context;

        public CustomerRepository(CustomerContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public void Add(Customer customer)
        {
            _context.Customers.Add(customer);
        }

        public async Task<IEnumerable<Customer>> GetAll()
        {
            return await _context.Customers.AsNoTracking().ToListAsync();
        }

        public async Task<Customer> GetByCPF(string cpf)
        {
            return await _context.Customers.FirstOrDefaultAsync(x => x.CPF.Number == cpf);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
