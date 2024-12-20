using CloupardTask.Domain.Models;
using System.Linq.Expressions;

namespace CloupardTask.DataAccess.Interfaces.Orders
{
    public interface IOrderRepository
    {
        Task<Order> CreateAsync(Order order);
        Task<Order> UpdateAsync(Order order);
        Task DeleteAsync(Order order);
        Task<Order?> GetAsync(Expression<Func<Order, bool>> predicate);
        IQueryable<Order> GetAll(Expression<Func<Order, bool>> predicate = null);
    }
}
