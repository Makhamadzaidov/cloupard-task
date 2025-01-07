using AutoMapper;
using CloupardTask.Api.DbContexts;
using CloupardTask.DataAccess.Interfaces.Orders;
using CloupardTask.DataAccess.Repositories.Orders;
using CloupardTask.Domain.Models;
using CloupardTask.Service.DTOs.Orders;
using CloupardTask.Service.Interfaces.Orders;
using CloupardTask.Service.ViewModels.Orders;
using System.Linq.Expressions;

namespace CloupardTask.Service.Services.Orders
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public OrderService(IMapper mapper, AppDbContext dbContext)
        {
            _appDbContext = dbContext;
            _orderRepository = new OrderRepository(_appDbContext);
            _mapper = mapper;
        }

        public async Task<OrderViewModel> CreateAsync(OrderCreateDto dto)
        {
            var order = _mapper.Map<Order>(dto);
            var createdOrder = await _orderRepository.CreateAsync(order);
            return _mapper.Map<OrderViewModel>(createdOrder);
        }

        public async Task<bool> DeleteAsync(int orderId)
        {
            var order = await _orderRepository.GetAsync(o => o.Id == orderId);
            if (order == null)
                return false;

            await _orderRepository.DeleteAsync(order);
            return true;
        }

        public async Task<IEnumerable<OrderViewModel>> GetAllAsync(Expression<Func<Order, bool>> predicate = null)
        {
            var orders = _orderRepository.GetAll(predicate);
            return _mapper.Map<IEnumerable<OrderViewModel>>(orders);
        }

        public async Task<OrderViewModel?> GetAsync(Expression<Func<Order, bool>> predicate)
        {
            var order = await _orderRepository.GetAsync(predicate);
            return order == null ? null : _mapper.Map<OrderViewModel>(order);
        }

        public async Task<OrderViewModel> UpdateAsync(int orderId, OrderUpdateDto dto)
        {
            var existingOrder = await _orderRepository.GetAsync(o => o.Id == orderId);
            if (existingOrder == null)
                throw new KeyNotFoundException("Order not found");

            existingOrder.OrderDate = dto.OrderDate ?? existingOrder.OrderDate;
            existingOrder.TotalAmount = dto.TotalAmount ?? existingOrder.TotalAmount;

            var updatedOrder = await _orderRepository.UpdateAsync(existingOrder);
            return _mapper.Map<OrderViewModel>(updatedOrder);
        }
    }
}
