using AutoMapper;
using LineTen.TechnicalTask.Data.DbContexts;
using LineTen.TechnicalTask.Data.Entities.Sql;
using LineTen.TechnicalTask.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace LineTen.TechnicalTask.Data.Repositories.Sql
{
    public class SqlOrderRepository : IOrderRepository
    {
        private readonly TechnicalTestContext _context;
        private readonly IMapper _mapper;

        public SqlOrderRepository(TechnicalTestContext context, IMapper mapper)
        {
            ArgumentNullException.ThrowIfNull(context);
            ArgumentNullException.ThrowIfNull(mapper);

            _context = context;
            _mapper = mapper;
        }

        public async Task<Order> AddOrderAsync(Order newOrder, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(newOrder);

            var entity = _mapper.Map<Order, OrderEntity>(newOrder);

            await _context.Orders.AddAsync(entity, cancellationToken).ConfigureAwait(false);

            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return _mapper.Map<OrderEntity, Order>(entity);
        }

        public async Task<bool> DeleteOrderAsync(int id, CancellationToken cancellationToken = default)
        {
            ArgumentOutOfRangeException.ThrowIfZero(id);

            var entity = await _context.Orders.FindAsync([id], cancellationToken).ConfigureAwait(false);

            if (entity is null)
            {
                return false;
            }

            _context.Orders.Remove(entity);

            _ = await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return true;
        }

        public async Task<ICollection<Order>> GetAllOrdersAsync(CancellationToken cancellationToken = default)
        {
            var orders = await _context.Orders
                .Select(o => _mapper.Map<OrderEntity, Order>(o))
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);

            return orders;
        }

        public async Task<Order?> GetOrderAsync(int id, CancellationToken cancellationToken = default)
        {
            ArgumentOutOfRangeException.ThrowIfZero(id);

            var entity = await _context.Orders.FindAsync([id], cancellationToken).ConfigureAwait(false);

            if (entity is null)
            {
                return null;
            }

            return _mapper.Map<OrderEntity, Order>(entity);
        }

        public async Task<Order?> UpdateOrderAsync(Order updatedOrder, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(updatedOrder);
            ArgumentOutOfRangeException.ThrowIfZero(updatedOrder.Id);

            var entity = await _context.Orders.FindAsync([updatedOrder.Id], cancellationToken).ConfigureAwait(false);

            if (entity is null)
            {
                return null;
            }

            _mapper.Map(updatedOrder, entity);

            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return _mapper.Map<OrderEntity, Order>(entity);
        }
    }
}