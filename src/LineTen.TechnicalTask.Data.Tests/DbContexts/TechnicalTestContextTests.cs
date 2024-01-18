using LineTen.TechnicalTask.Data.Tests.Entities.Sql;
using LineTen.TechnicalTask.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace LineTen.TechnicalTask.Data.Tests.DbContexts
{
    public class TechnicalTestContext(DbContextOptions<TechnicalTestContext> options) : DbContext(options)
    {
        public DbSet<CustomerEntity> Customers { get; set; } = null!;

        public DbSet<ProductEntity> Products { get; set; } = null!;

        public DbSet<OrderEntity> Orders { get; set; } = null!;
    }

    public class TechnicalTestContextFixture : IDisposable
    {
        public DbContextOptions<TechnicalTestContext> DbContextOptions { get; }

        public TechnicalTestContextFixture()
        {
            DbContextOptions = new DbContextOptionsBuilder<TechnicalTestContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryTechnicalTestDatabase")
                .Options;

            using var dbContext = new TechnicalTestContext(DbContextOptions);
            dbContext.Database.EnsureCreated();
        }

        public TechnicalTestContext CreateDbContext()
        {
            return new TechnicalTestContext(DbContextOptions);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                using var dbContext = new TechnicalTestContext(DbContextOptions);
                dbContext.Database.EnsureDeleted();
            }
        }
    }

    public class TechnicalTestContextTests : IClassFixture<TechnicalTestContextFixture>
    {
        private readonly TechnicalTestContextFixture _fixture;

        private static readonly CustomerEntity _customerEntity = new()
        {
            Id = 1,
            FirstName = "John",
            LastName = "Doe",
            Phone = "078 0156 5740",
            Email = "john.doe@lineten.com"
        };

        private static readonly ProductEntity _productEntity = new()
        {
            Id = 1,
            Name = "Awesome Product",
            Description = "This is a really awesome product!",
            SKU = "ABC-12345-S-BL"
        };

        private static readonly OrderEntity _orderEntity = new()
        {
            Id = 1,
            CustomerId = _customerEntity.Id,
            ProductId = _productEntity.Id,
            Status = (int)OrderStatus.New,
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow
        };

        public TechnicalTestContextTests(TechnicalTestContextFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void CanConstruct()
        {
            using var instance = new TechnicalTestContext(_fixture.DbContextOptions);
            instance.Should().NotBeNull();
        }

        [Fact]
        public void CanSetAndGetCustomers()
        {
            // Act
            using (var dbContext = _fixture.CreateDbContext())
            {
                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();
                dbContext.Customers.Add(_customerEntity);
                dbContext.SaveChanges();
            }

            // Assert
            using (var dbContext = _fixture.CreateDbContext())
            {
                var savedCustomer = dbContext.Customers.Single();
                savedCustomer.FirstName.Should().Be(_customerEntity.FirstName);
                savedCustomer.LastName.Should().Be(_customerEntity.LastName);
                savedCustomer.Phone.Should().Be(_customerEntity.Phone);
                savedCustomer.Email.Should().Be(_customerEntity.Email);
            }
        }

        [Fact]
        public void CanUpdateCustomer()
        {
            // Arrange
            const string newName = "Jane";

            // Safety net
            newName.Should().NotBe(_customerEntity.FirstName);

            using (var dbContext = _fixture.CreateDbContext())
            {
                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();
                dbContext.Customers.Add(_customerEntity);
                dbContext.SaveChanges();
            }

            // Act
            using (var dbContext = _fixture.CreateDbContext())
            {
                var customerToUpdate = dbContext.Customers.Single();
                customerToUpdate.FirstName = newName;
                dbContext.SaveChanges();
            }

            // Assert        
            using (var dbContext = _fixture.CreateDbContext())
            {
                var updatedCustomer = dbContext.Customers.Single();
                updatedCustomer.FirstName.Should().Be(newName);
                updatedCustomer.LastName.Should().Be(_customerEntity.LastName);
            }
        }

        [Fact]
        public void CanDeleteCustomer()
        {
            // Arrange
            using (var dbContext = _fixture.CreateDbContext())
            {
                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();
                dbContext.Customers.Add(_customerEntity);
                dbContext.SaveChanges();
            }

            // Act
            using (var dbContext = _fixture.CreateDbContext())
            {
                var customerToDelete = dbContext.Customers.Single();
                dbContext.Customers.Remove(customerToDelete);
                dbContext.SaveChanges();
            }

            // Assert
            using (var dbContext = _fixture.CreateDbContext())
            {
                dbContext.Customers.Should().BeEmpty();
            }
        }

        [Fact]
        public void CanSetAndGetProducts()
        {
            // Act
            using (var dbContext = _fixture.CreateDbContext())
            {
                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();
                dbContext.Products.Add(_productEntity);
                dbContext.SaveChanges();
            }

            // Assert
            using (var dbContext = _fixture.CreateDbContext())
            {
                var savedProduct = dbContext.Products.Single();
                savedProduct.Name.Should().Be(_productEntity.Name);
                savedProduct.Description.Should().Be(_productEntity.Description);
                savedProduct.SKU.Should().Be(_productEntity.SKU);
            }
        }

        [Fact]
        public void CanUpdateProduct()
        {
            // Arrange
            const string newDescription = "Improved description";

            // Safety net
            newDescription.Should().NotBe(_productEntity.Description);

            using (var dbContext = _fixture.CreateDbContext())
            {
                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();
                dbContext.Products.Add(_productEntity);
                dbContext.SaveChanges();
            }

            // Act
            using (var dbContext = _fixture.CreateDbContext())
            {
                var productToUpdate = dbContext.Products.Single();
                productToUpdate.Description = newDescription;
                dbContext.SaveChanges();
            }

            // Assert        
            using (var dbContext = _fixture.CreateDbContext())
            {
                var updatedProduct = dbContext.Products.Single();
                updatedProduct.Description.Should().Be(newDescription);
                updatedProduct.Name.Should().Be(_productEntity.Name);
            }
        }

        [Fact]
        public void CanDeleteProduct()
        {
            // Arrange
            using (var dbContext = _fixture.CreateDbContext())
            {
                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();
                dbContext.Products.Add(_productEntity);
                dbContext.SaveChanges();
            }

            // Act
            using (var dbContext = _fixture.CreateDbContext())
            {
                var productToDelete = dbContext.Products.Single();
                dbContext.Products.Remove(productToDelete);
                dbContext.SaveChanges();
            }

            // Assert
            using (var dbContext = _fixture.CreateDbContext())
            {
                dbContext.Products.Should().BeEmpty();
            }
        }

        [Fact]
        public void CanSetAndGetOrders()
        {
            // Arrange
            using (var dbContext = _fixture.CreateDbContext())
            {
                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();

                // Add customer and product to the database
                dbContext.Customers.Add(_customerEntity);
                dbContext.Products.Add(_productEntity);
                dbContext.SaveChanges();
            }

            // Act
            using (var dbContext = _fixture.CreateDbContext())
            {
                dbContext.Orders.Add(_orderEntity);
                dbContext.SaveChanges();
            }

            // Assert
            using (var dbContext = _fixture.CreateDbContext())
            {
                var savedOrder = dbContext.Orders.Single();
                savedOrder.Status.Should().Be(_orderEntity.Status);
                savedOrder.CreatedDate.Should().Be(savedOrder.CreatedDate);
                savedOrder.UpdatedDate.Should().Be(savedOrder.UpdatedDate);
            }
        }
        
        [Fact]
        public void CanUpdateOrderStatus()
        {
            // Arrange
            const int newStatus = (int)OrderStatus.Completed;

            // Safety net
            newStatus.Should().NotBe(_orderEntity.Status);

            using (var dbContext = _fixture.CreateDbContext())
            {
                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();

                // Add customer, product, and order to the database
                dbContext.Customers.Add(_customerEntity);
                dbContext.Products.Add(_productEntity);
                dbContext.Orders.Add(_orderEntity);
                dbContext.SaveChanges();
            }

            // Act
            using (var dbContext = _fixture.CreateDbContext())
            {
                var orderToUpdate = dbContext.Orders.Single();
                orderToUpdate.Status = newStatus;
                orderToUpdate.UpdatedDate = DateTime.UtcNow;
                dbContext.SaveChanges();
            }

            // Assert        
            using (var dbContext = _fixture.CreateDbContext())
            {
                var updatedOrder = dbContext.Orders.Single();
                updatedOrder.Status.Should().Be(newStatus);
                updatedOrder.CreatedDate.Should().Be(updatedOrder.CreatedDate);
                updatedOrder.UpdatedDate.Should().NotBe(_orderEntity.UpdatedDate);
            }
        }

        [Fact]
        public void CanDeleteOrder()
        {
            // Arrange
            using (var dbContext = _fixture.CreateDbContext())
            {
                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();

                // Add customer, product, and order to the database
                dbContext.Customers.Add(_customerEntity);
                dbContext.Products.Add(_productEntity);
                dbContext.Orders.Add(_orderEntity);
                dbContext.SaveChanges();
            }

            // Act
            using (var dbContext = _fixture.CreateDbContext())
            {
                var orderToDelete = dbContext.Orders.Single();
                dbContext.Orders.Remove(orderToDelete);
                dbContext.SaveChanges();
            }

            // Assert
            using (var dbContext = _fixture.CreateDbContext())
            {
                dbContext.Orders.Should().BeEmpty();
            }
        }
    }
}