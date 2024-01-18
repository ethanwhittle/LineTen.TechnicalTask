using LineTen.TechnicalTask.Domain.Models;

namespace LineTen.TechnicalTask.Data.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> AddOrderAsync(Order newOrder, CancellationToken cancellationToken = default);

        Task<bool> DeleteOrderAsync(int id, CancellationToken cancellationToken = default);

        // ENHANCE: Use case for GetAllOrdersAsync(int customerId) or GetAllOrdersAsync(int productId)?
        Task<ICollection<Order>> GetAllOrdersAsync(CancellationToken cancellationToken = default);

        Task<Order?> GetOrderAsync(int id, CancellationToken cancellationToken = default);

        Task<Order?> UpdateOrderAsync(Order updatedOrder, CancellationToken cancellationToken = default);
    }
}