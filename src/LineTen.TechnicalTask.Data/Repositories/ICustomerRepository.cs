using LineTen.TechnicalTask.Domain.Models;

namespace LineTen.TechnicalTask.Data.Repositories
{
    public interface ICustomerRepository
    {
        Task<Customer> AddCustomerAsync(Customer newCustomer, CancellationToken cancellationToken = default);

        Task<bool> DeleteCustomerAsync(int id, CancellationToken cancellationToken = default);

        Task<ICollection<Customer>> GetAllCustomersAsync(CancellationToken cancellationToken = default);

        Task<Customer?> GetCustomerAsync(int id, CancellationToken cancellationToken = default);

        Task<Customer?> UpdateCustomerAsync(Customer updatedCustomer, CancellationToken cancellationToken = default);
    }
}