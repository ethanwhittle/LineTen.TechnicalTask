using LineTen.TechnicalTask.Data.Repositories;
using LineTen.TechnicalTask.Domain.Models;

namespace LineTen.TechnicalTask.Service.Tests.Services
{
    public interface ICustomerService
    {
        Task<Customer> AddCustomerAsync(Customer newCustomer, CancellationToken cancellationToken = default);

        Task<bool> DeleteCustomerAsync(int id, CancellationToken cancellationToken = default);

        Task<ICollection<Customer>> GetAllCustomersAsync(CancellationToken cancellationToken = default);

        Task<Customer?> GetCustomerAsync(int id, CancellationToken cancellationToken = default);

        Task<Customer?> UpdateCustomerAsync(Customer updatedCustomer, CancellationToken cancellationToken = default);
    }

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

    public class CustomerServiceTests
    {
        private readonly CustomerService _testClass;
        private readonly ICustomerRepository _customerRepository;

        public CustomerServiceTests()
        {
            _customerRepository = Substitute.For<ICustomerRepository>();
            _testClass = new CustomerService(_customerRepository);
        }

        [Fact]
        public void CanConstruct()
        {
            // Act
            var instance = new CustomerService(_customerRepository);

            // Assert
            instance.Should().NotBeNull();
        }

        [Fact]
        public void CannotConstructWithNullCustomerRepository()
        {
            FluentActions.Invoking(() => new CustomerService(default(ICustomerRepository))).Should().Throw<ArgumentNullException>().WithParameterName("customerRepository");
        }

        [Fact]
        public async Task CanCallAddCustomerAsync()
        {
            // Arrange
            var customer = new Customer
            {
                Id = 617774081,
                FirstName = "TestValue1095821463",
                LastName = "TestValue1429985347",
                Phone = "TestValue1795522747",
                Email = "TestValue689943185"
            };

            _customerRepository.AddCustomerAsync(Arg.Any<Customer>(), Arg.Any<CancellationToken>()).Returns(customer);

            // Act
            var result = await _testClass.AddCustomerAsync(customer, CancellationToken.None);

            // Assert
            await _customerRepository.Received(1).AddCustomerAsync(Arg.Any<Customer>(), Arg.Any<CancellationToken>());

            result.Should().BeEquivalentTo(customer);
        }

        [Fact]
        public async Task CannotCallAddCustomerAsyncWithNullNewCustomer()
        {
            await FluentActions.Invoking(() => _testClass.AddCustomerAsync(default(Customer), CancellationToken.None)).Should().ThrowAsync<ArgumentNullException>().WithParameterName("newCustomer");
        }

        [Fact]
        public async Task CanCallDeleteCustomerAsync()
        {
            // Arrange
            var id = 1646932882;

            _customerRepository.DeleteCustomerAsync(Arg.Any<int>(), Arg.Any<CancellationToken>()).Returns(true);

            // Act
            var result = await _testClass.DeleteCustomerAsync(id, CancellationToken.None);

            // Assert
            await _customerRepository.Received(1).DeleteCustomerAsync(id, Arg.Any<CancellationToken>());

            result.Should().BeTrue();
        }

        [Fact]
        public async Task CanCallGetAllCustomersAsync()
        {
            // Arrange
            var customers = Substitute.For<ICollection<Customer>>();

            _customerRepository.GetAllCustomersAsync(Arg.Any<CancellationToken>()).Returns(customers);

            // Act
            var result = await _testClass.GetAllCustomersAsync(CancellationToken.None);

            // Assert
            await _customerRepository.Received(1).GetAllCustomersAsync(Arg.Any<CancellationToken>());

            result.Should().BeEquivalentTo(customers);
        }

        [Fact]
        public async Task CanCallGetCustomerAsync()
        {
            // Arrange
            var id = 1211105727;

            var customer = new Customer
            {
                Id = 1211105727,
                FirstName = "TestValue1225268810",
                LastName = "TestValue1062250519",
                Phone = "TestValue338481401",
                Email = "TestValue966848679"
            };

            _customerRepository.GetCustomerAsync(Arg.Any<int>(), Arg.Any<CancellationToken>()).Returns(customer);

            // Act
            var result = await _testClass.GetCustomerAsync(id, CancellationToken.None);

            // Assert
            await _customerRepository.Received(1).GetCustomerAsync(id, Arg.Any<CancellationToken>());

            result.Should().BeEquivalentTo(customer);
        }

        [Fact]
        public async Task CanCallUpdateCustomerAsync()
        {
            // Arrange
            var updatedCustomer = new Customer
            {
                Id = 571991254,
                FirstName = "TestValue942507666",
                LastName = "TestValue1916310546",
                Phone = "TestValue1416069532",
                Email = "TestValue625115488"
            };

            _customerRepository.UpdateCustomerAsync(Arg.Any<Customer>(), Arg.Any<CancellationToken>()).Returns(updatedCustomer);

            // Act
            var result = await _testClass.UpdateCustomerAsync(updatedCustomer, CancellationToken.None);

            // Assert
            await _customerRepository.Received(1).UpdateCustomerAsync(Arg.Any<Customer>(), Arg.Any<CancellationToken>());

            result.Should().BeEquivalentTo(updatedCustomer);
        }

        [Fact]
        public async Task CannotCallUpdateCustomerAsyncWithNullUpdatedCustomer()
        {
            await FluentActions.Invoking(() => _testClass.UpdateCustomerAsync(default(Customer), CancellationToken.None)).Should().ThrowAsync<ArgumentNullException>().WithParameterName("updatedCustomer");
        }

        [Fact]
        public async Task CannotCallUpdateCustomerAsyncWithDefaultId()
        {
            // Arrange
            var invalidCustomer = new Customer
            {
                Id = default,
                FirstName = "InvalidCustomerFirstName",
                LastName = "InvalidCustomerLastName",
                Phone = "InvalidCustomerPhone",
                Email = "InvalidCustomerEmail"
            };

            // Assert
            await FluentActions.Invoking(() => _testClass.UpdateCustomerAsync(invalidCustomer, CancellationToken.None)).Should().ThrowAsync<ArgumentOutOfRangeException>().WithParameterName("updatedCustomer.Id");
        }
    }
}