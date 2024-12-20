using CloupardTask.Api.DbContexts;
using CloupardTask.DataAccess.Interfaces.Customers;
using CloupardTask.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CloupardTask.DataAccess.Repositories.Customers
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _dbContext;

        public CustomerRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Customer> CreateAsync(Customer customer)
        {
            var entity = await _dbContext.Customers.AddAsync(customer);
            await _dbContext.SaveChangesAsync();
            return entity.Entity;
        }

        public async Task<Customer> UpdateAsync(Customer customer)
        {
            var existingCustomer = await _dbContext.Customers.FindAsync(customer.Id);
            if (existingCustomer == null)
                throw new KeyNotFoundException("Customer not found.");

            _dbContext.Entry(existingCustomer).CurrentValues.SetValues(customer);
            await _dbContext.SaveChangesAsync();

            return existingCustomer;
        }

        public async Task DeleteAsync(Customer customer)
        {
            _dbContext.Customers.Remove(customer);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Customer?> GetAsync(Expression<Func<Customer, bool>> predicate)
        {
            return await _dbContext.Customers.FirstOrDefaultAsync(predicate);
        }

        public IQueryable<Customer> GetAll(Expression<Func<Customer, bool>>? predicate = null)
        {
            return predicate == null
                ? _dbContext.Customers
                : _dbContext.Customers.Where(predicate);
        }
    }
}
