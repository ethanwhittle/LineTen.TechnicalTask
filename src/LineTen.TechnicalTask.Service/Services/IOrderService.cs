using LineTen.TechnicalTask.Domain.Models;

namespace LineTen.TechnicalTask.Service.Services
{
    public interface IOrderService
    {
        Task<Order> AddOrderAsync(Order newOrder, CancellationToken cancellationToken = default);

        Task<bool> DeleteOrderAsync(int id, CancellationToken cancellationToken = default);

        Task<ICollection<Order>> GetAllOrdersAsync(CancellationToken cancellationToken = default);

        Task<Order?> GetOrderAsync(int id, CancellationToken cancellationToken = default);

        Task<Order?> UpdateOrderAsync(Order updatedOrder, CancellationToken cancellationToken = default);
    }
}