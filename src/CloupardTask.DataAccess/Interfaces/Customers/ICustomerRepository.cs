using CloupardTask.Domain.Models;
using System.Linq.Expressions;

namespace CloupardTask.DataAccess.Interfaces.Customers
{
    public interface ICustomerRepository
    {
        Task<Customer> CreateAsync(Customer customer);
        Task<Customer> UpdateAsync(Customer customer);
        Task DeleteAsync(Customer customer);
        Task<Customer?> GetAsync(Expression<Func<Customer, bool>> predicate);
        IQueryable<Customer> GetAll(Expression<Func<Customer, bool>>? predicate = null);
    }
}
