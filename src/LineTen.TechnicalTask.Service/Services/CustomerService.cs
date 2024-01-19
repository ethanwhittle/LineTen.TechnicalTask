using LineTen.TechnicalTask.Data.Repositories;
using LineTen.TechnicalTask.Domain.Models;

namespace LineTen.TechnicalTask.Service.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            ArgumentNullException.ThrowIfNull(customerRepository);

            _customerRepository = customerRepository;
        }

        public Task<Customer> AddCustomerAsync(Customer newCustomer, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(newCustomer, nameof(newCustomer));

            return _customerRepository.AddCustomerAsync(newCustomer, cancellationToken);
        }

        public Task<bool> DeleteCustomerAsync(int id, CancellationToken cancellationToken = default)
        {
            ArgumentOutOfRangeException.ThrowIfZero(id);

            return _customerRepository.DeleteCustomerAsync(id, cancellationToken);
        }

        public Task<ICollection<Customer>> GetAllCustomersAsync(CancellationToken cancellationToken = default)
        {
            return _customerRepository.GetAllCustomersAsync(cancellationToken);
        }

        public Task<Customer?> GetCustomerAsync(int id, CancellationToken cancellationToken = default)
        {
            ArgumentOutOfRangeException.ThrowIfZero(id);

            return _customerRepository.GetCustomerAsync(id, cancellationToken);
        }

        public Task<Customer?> UpdateCustomerAsync(Customer updatedCustomer, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(updatedCustomer, nameof(updatedCustomer));
            ArgumentOutOfRangeException.ThrowIfZero(updatedCustomer.Id);

            return _customerRepository.UpdateCustomerAsync(updatedCustomer, cancellationToken);
        }
    }
}