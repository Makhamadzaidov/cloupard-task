using CloupardTask.Api.DbContexts;
using CloupardTask.DataAccess.Interfaces.Orders;
using CloupardTask.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CloupardTask.DataAccess.Repositories.Orders
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _dbContext;

        public OrderRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Order> CreateAsync(Order order)
        {
            var entity = await _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync();
            return entity.Entity;
        }

        public async Task DeleteAsync(Order order)
        {
            _dbContext.Orders.Remove(order);
            await _dbContext.SaveChangesAsync();
        }

        public IQueryable<Order> GetAll(Expression<Func<Order, bool>> predicate = null)
        {
            var query = _dbContext.Orders
                .Include(p => p.OrderDetails)
                .ThenInclude(od => od.Product)
                .Include(p => p.Customer);

            return predicate == null ? query : query.Where(predicate);
        }


        public async Task<Order?> GetAsync(Expression<Func<Order, bool>> predicate)
        {
            return await _dbContext.Orders.Include(p => p.OrderDetails).ThenInclude(od => od.Product).Include(p => p.Customer).FirstOrDefaultAsync(predicate);
        }

        public async Task<Order> UpdateAsync(Order order)
        {
            var existingOrder = await _dbContext.Orders.FindAsync(order.Id);
            if (existingOrder != null)
            {
                _dbContext.Entry(existingOrder).CurrentValues.SetValues(order);
                await _dbContext.SaveChangesAsync();
            }
            return existingOrder;
        }
    }
}
