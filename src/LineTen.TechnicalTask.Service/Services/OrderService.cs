using LineTen.TechnicalTask.Data.Repositories;
using LineTen.TechnicalTask.Domain.Models;

namespace LineTen.TechnicalTask.Service.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            ArgumentNullException.ThrowIfNull(orderRepository);

            _orderRepository = orderRepository;
        }

        public Task<Order> AddOrderAsync(Order newOrder, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(newOrder, nameof(newOrder));

            return _orderRepository.AddOrderAsync(newOrder, cancellationToken);
        }

        public Task<bool> DeleteOrderAsync(int id, CancellationToken cancellationToken = default)
        {
            ArgumentOutOfRangeException.ThrowIfZero(id);

            return _orderRepository.DeleteOrderAsync(id, cancellationToken);
        }

        public Task<ICollection<Order>> GetAllOrdersAsync(CancellationToken cancellationToken = default)
        {
            return _orderRepository.GetAllOrdersAsync(cancellationToken);
        }

        public Task<Order?> GetOrderAsync(int id, CancellationToken cancellationToken = default)
        {
            ArgumentOutOfRangeException.ThrowIfZero(id);

            return _orderRepository.GetOrderAsync(id, cancellationToken);
        }

        public Task<Order?> UpdateOrderAsync(Order updatedOrder, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(updatedOrder, nameof(updatedOrder));
            ArgumentOutOfRangeException.ThrowIfZero(updatedOrder.Id);

            return _orderRepository.UpdateOrderAsync(updatedOrder, cancellationToken);
        }
    }
}