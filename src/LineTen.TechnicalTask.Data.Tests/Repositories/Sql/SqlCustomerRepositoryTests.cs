using AutoMapper;
using LineTen.TechnicalTask.Data.DbContexts;
using LineTen.TechnicalTask.Data.Entities.Sql;
using LineTen.TechnicalTask.Data.Mappings;
using LineTen.TechnicalTask.Data.Tests.Fixtures;
using LineTen.TechnicalTask.Data.Tests.Helpers;
using LineTen.TechnicalTask.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace LineTen.TechnicalTask.Data.Tests.Repositories.Sql
{
    public interface ICustomerRepository
    {
        Task<Customer> AddCustomerAsync(Customer newCustomer, CancellationToken cancellationToken = default);

        Task<bool> DeleteCustomerAsync(int id, CancellationToken cancellationToken = default);

        Task<ICollection<Customer>> GetAllCustomersAsync(CancellationToken cancellationToken = default);

        Task<Customer?> GetCustomerAsync(int id, CancellationToken cancellationToken = default);

        Task<Customer?> UpdateCustomerAsync(Customer updatedCustomer, CancellationToken cancellationToken = default);
    }

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

    public class SqlCustomerRepositoryTests : IClassFixture<TechnicalTestContextFixture>
    {
        private readonly SqlCustomerRepository _testClass;
        private readonly TechnicalTestContextFixture _fixture;
        private readonly TechnicalTestContext _context;
        private readonly IMapper _mapper;

        public SqlCustomerRepositoryTests(TechnicalTestContextFixture fixture)
        {
            _fixture = fixture;
            _context = fixture.CreateDbContext();
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<EntityModelProfile>()).CreateMapper();
            _testClass = new SqlCustomerRepository(_context, _mapper);
        }

        [Fact]
        public void CanConstruct()
        {
            // Act
            var instance = new SqlCustomerRepository(_context, _mapper);

            // Assert
            instance.Should().NotBeNull();
        }

        [Fact]
        public void CannotConstructWithNullContext()
        {
            FluentActions.Invoking(() => new SqlCustomerRepository(default(TechnicalTestContext), _mapper)).Should().Throw<ArgumentNullException>().WithParameterName("context");
        }

        [Fact]
        public void CannotConstructWithNullMapper()
        {
            FluentActions.Invoking(() => new SqlCustomerRepository(_context, default(IMapper))).Should().Throw<ArgumentNullException>().WithParameterName("mapper");
        }

        [Fact]
        public async Task CanCallAddCustomerAsync()
        {
            // Arrange
            var customer = TestHelper.CreateCustomer(1);

            using (var dbContext = _fixture.CreateDbContext())
            {
                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();
            }

            // Act
            var result = await _testClass.AddCustomerAsync(customer, CancellationToken.None);

            // Assert
            result.Should().BeEquivalentTo(customer);

            using (var dbContext = _fixture.CreateDbContext())
            {
                var savedCustomer = dbContext.Customers.Single();
                savedCustomer.Id.Should().Be(customer.Id);
                savedCustomer.FirstName.Should().Be(customer.FirstName);
                savedCustomer.LastName.Should().Be(customer.LastName);
                savedCustomer.Phone.Should().Be(customer.Phone);
                savedCustomer.Email.Should().Be(customer.Email);
            }
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
            var customer = TestHelper.CreateCustomerEntity(1);

            using (var dbContext = _fixture.CreateDbContext())
            {
                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();
                dbContext.Customers.Add(customer);
                dbContext.SaveChanges();
            }

            // Act
            var result = await _testClass.DeleteCustomerAsync(1, CancellationToken.None);

            // Assert
            result.Should().BeTrue();

            using (var dbContext = _fixture.CreateDbContext())
            {
                var savedCustomer = dbContext.Customers.Should().BeEmpty();
            }
        }

        [Fact]
        public async Task CanCallGetAllCustomersAsync()
        {
            // Arrange
            var customers = Enumerable.Range(1, 10)
                .Select(c => TestHelper.CreateCustomerEntity(c))
                .ToList();

            using (var dbContext = _fixture.CreateDbContext())
            {
                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();
                dbContext.Customers.AddRange(customers);
                dbContext.SaveChanges();
            }

            // Act
            var result = await _testClass.GetAllCustomersAsync(CancellationToken.None);

            // Assert
            result.Should().NotBeEmpty().And.HaveCount(10);
        }

        [Fact]
        public async Task CanCallGetCustomerAsync()
        {
            // Arrange
            var customer = TestHelper.CreateCustomerEntity(1);

            using (var dbContext = _fixture.CreateDbContext())
            {
                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();
                dbContext.Customers.Add(customer);
                dbContext.SaveChanges();
            }

            // Act
            var result = await _testClass.GetCustomerAsync(1, CancellationToken.None);

            // Assert
            result.Should().NotBeNull().And.BeOfType<Customer>();
            result!.Id.Should().Be(customer.Id);
            result!.FirstName.Should().Be(customer.FirstName);
            result!.LastName.Should().Be(customer.LastName);
            result!.Phone.Should().Be(customer.Phone);
            result!.Email.Should().Be(customer.Email);
        }

        [Fact]
        public async Task CanCallUpdateCustomerAsync()
        {
            // Arrange
            var customer = TestHelper.CreateCustomerEntity(1);

            using (var dbContext = _fixture.CreateDbContext())
            {
                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();
                dbContext.Customers.Add(customer);
                dbContext.SaveChanges();
            }

            var updatedCustomer = new Customer
            {
                Id = 1,
                FirstName = $"NewCustomer1FirstName",
                LastName = $"NewCustomer1LastName",
                Phone = $"NewCustomer1Phone",
                Email = $"NewCustomer1Email"
            };

            // Act
            var result = await _testClass.UpdateCustomerAsync(updatedCustomer, CancellationToken.None);

            // Assert
            result.Should().BeEquivalentTo(updatedCustomer);

            using (var dbContext = _fixture.CreateDbContext())
            {
                var savedCustomer = dbContext.Customers.Single();
                savedCustomer.Id.Should().Be(updatedCustomer.Id);
                savedCustomer.FirstName.Should().Be(updatedCustomer.FirstName);
                savedCustomer.LastName.Should().Be(updatedCustomer.LastName);
                savedCustomer.Phone.Should().Be(updatedCustomer.Phone);
                savedCustomer.Email.Should().Be(updatedCustomer.Email);
            }
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

            using (var dbContext = _fixture.CreateDbContext())
            {
                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();
            }

            // Act
            await FluentActions.Invoking(() => _testClass.UpdateCustomerAsync(invalidCustomer, CancellationToken.None)).Should().ThrowAsync<ArgumentOutOfRangeException>().WithParameterName("updatedCustomer.Id");

            // Assert
            using (var dbContext = _fixture.CreateDbContext())
            {
                dbContext.Customers.Should().BeEmpty();
            }
        }
    }
}