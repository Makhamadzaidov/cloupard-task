using CloupardTask.Domain.Models;
using CloupardTask.Service.DTOs.Orders;
using CloupardTask.Service.ViewModels.Orders;
using System.Linq.Expressions;

namespace CloupardTask.Service.Interfaces.Orders
{
    public interface IOrderService
    {
        Task<OrderViewModel> CreateAsync(OrderCreateDto dto);
        Task<OrderViewModel> UpdateAsync(int orderId, OrderUpdateDto dto);
        Task<bool> DeleteAsync(int orderId);
        Task<OrderViewModel?> GetAsync(Expression<Func<Order, bool>> predicate);
        Task<IEnumerable<OrderViewModel>> GetAllAsync(Expression<Func<Order, bool>> predicate = null);
    }
}
