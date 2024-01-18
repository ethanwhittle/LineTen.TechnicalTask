using AutoMapper;
using LineTen.TechnicalTask.Data.DbContexts;
using LineTen.TechnicalTask.Data.Entities.Sql;
using LineTen.TechnicalTask.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace LineTen.TechnicalTask.Data.Repositories.Sql
{
    public class SqlCustomerRepository : ICustomerRepository
    {
        private readonly TechnicalTestContext _context;
        private readonly IMapper _mapper;

        public SqlCustomerRepository(TechnicalTestContext context, IMapper mapper)
        {
            ArgumentNullException.ThrowIfNull(context);
            ArgumentNullException.ThrowIfNull(mapper);

            _context = context;
            _mapper = mapper;
        }

        public async Task<Customer> AddCustomerAsync(Customer newCustomer, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(newCustomer);

            var entity = _mapper.Map<Customer, CustomerEntity>(newCustomer);

            await _context.Customers.AddAsync(entity, cancellationToken).ConfigureAwait(false);

            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return _mapper.Map<CustomerEntity, Customer>(entity);
        }

        public async Task<bool> DeleteCustomerAsync(int id, CancellationToken cancellationToken = default)
        {
            ArgumentOutOfRangeException.ThrowIfZero(id);

            var entity = await _context.Customers.FindAsync([id], cancellationToken: cancellationToken).ConfigureAwait(false);

            if (entity is null)
            {
                return false;
            }

            _context.Customers.Remove(entity);

            _ = await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return true;
        }

        public async Task<ICollection<Customer>> GetAllCustomersAsync(CancellationToken cancellationToken = default)
        {
            // Enhance: May want to consider using an IEnumerable depending on how many customers we intend to support
            var customers = await _context.Customers
                .Select(c => _mapper.Map<CustomerEntity, Customer>(c))
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);

            return customers;
        }

        public async Task<Customer?> GetCustomerAsync(int id, CancellationToken cancellationToken = default)
        {
            ArgumentOutOfRangeException.ThrowIfZero(id);

            var entity = await _context.Customers.FindAsync([id], cancellationToken: cancellationToken).ConfigureAwait(false);

            if (entity is null)
            {
                // TODO: Determine how we want to handle these
                return null;
            }

            return _mapper.Map<CustomerEntity, Customer>(entity);
        }

        public async Task<Customer?> UpdateCustomerAsync(Customer updatedCustomer, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(updatedCustomer);
            ArgumentOutOfRangeException.ThrowIfZero(updatedCustomer.Id);

            var entity = await _context.Customers.FindAsync([updatedCustomer.Id], cancellationToken).ConfigureAwait(false);

            if (entity is null)
            {
                // TODO: Determine how we want to handle these
                return null;
            }

            _mapper.Map(updatedCustomer, entity);

            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return _mapper.Map<CustomerEntity, Customer>(entity);
        }
    }
}