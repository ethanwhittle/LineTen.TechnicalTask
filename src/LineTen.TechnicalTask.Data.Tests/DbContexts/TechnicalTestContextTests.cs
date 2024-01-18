using LineTen.TechnicalTask.Data.DbContexts;
using LineTen.TechnicalTask.Data.Entities.Sql;
using LineTen.TechnicalTask.Data.Tests.Fixtures;
using LineTen.TechnicalTask.Data.Tests.Helpers;
using LineTen.TechnicalTask.Domain.Enums;

namespace LineTen.TechnicalTask.Data.Tests.DbContexts
{
    public class TechnicalTestContextTests : IClassFixture<TechnicalTestContextFixture>
    {
        private readonly TechnicalTestContextFixture _fixture;

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
        public void CanAddAndUpdateMultipleCustomers()
        {
            // Arrange
            var customers = Enumerable.Range(1, 10)
                .Select(TestHelper.CreateCustomerEntity)
                .ToList();

            using (var dbContext = _fixture.CreateDbContext())
            {
                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();
                dbContext.Customers.AddRange(customers);
                dbContext.SaveChanges();
            }

            // Act
            using (var dbContext = _fixture.CreateDbContext())
            {
                foreach (var customer in customers)
                {
                    var customerToUpdate = dbContext.Customers.Single(c => c.Id == customer.Id);
                    customerToUpdate.FirstName = "Updated" + customer.FirstName;
                }

                dbContext.SaveChanges();
            }

            // Assert
            using (var dbContext = _fixture.CreateDbContext())
            {
                foreach (var customer in customers)
                {
                    var updatedCustomer = dbContext.Customers.Single(c => c.Id == customer.Id);
                    updatedCustomer.FirstName.Should().Be("Updated" + customer.FirstName);
                }
            }
        }

        [Fact]
        public void CanDeleteSomeCustomersAndCheckRemaining()
        {
            // Arrange
            var customers = Enumerable.Range(1, 10)
                .Select(TestHelper.CreateCustomerEntity)
                .ToList();

            using (var dbContext = _fixture.CreateDbContext())
            {
                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();

                dbContext.Customers.AddRange(customers);
                dbContext.SaveChanges();
            }

            // Act
            using (var dbContext = _fixture.CreateDbContext())
            {
                var customersToDelete = dbContext.Customers.Where(c => c.Id % 2 == 0);
                dbContext.Customers.RemoveRange(customersToDelete);
                dbContext.SaveChanges();
            }

            var expectedRemainingCustomers = customers.Where(c => c.Id % 2 != 0);

            // Assert
            using (var dbContext = _fixture.CreateDbContext())
            {
                var remainingCustomers = dbContext.Customers.ToList();
                remainingCustomers.Should().HaveCount(5);
                remainingCustomers.Should().BeEquivalentTo(expectedRemainingCustomers, options => options.Excluding(o => o.Orders));
            }
        }

        [Fact]
        public void CanAddAndUpdateMultipleProducts()
        {
            // Arrange
            var products = Enumerable.Range(1, 10)
                .Select(TestHelper.CreateProductEntity)
                .ToList();

            using (var dbContext = _fixture.CreateDbContext())
            {
                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();
                dbContext.Products.AddRange(products);
                dbContext.SaveChanges();
            }

            // Act
            using (var dbContext = _fixture.CreateDbContext())
            {
                foreach (var product in products)
                {
                    var productToUpdate = dbContext.Products.Single(p => p.Id == product.Id);
                    productToUpdate.Description = "Updated" + product.Description;
                }

                dbContext.SaveChanges();
            }

            // Assert
            using (var dbContext = _fixture.CreateDbContext())
            {
                foreach (var product in products)
                {
                    var updatedProduct = dbContext.Products.Single(p => p.Id == product.Id);
                    updatedProduct.Description.Should().Be("Updated" + product.Description);
                }
            }
        }

        [Fact]
        public void CanDeleteSomeProductsAndCheckRemaining()
        {
            // Arrange
            var products = Enumerable.Range(1, 10)
                .Select(TestHelper.CreateProductEntity)
                .ToList();

            using (var dbContext = _fixture.CreateDbContext())
            {
                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();

                dbContext.Products.AddRange(products);
                dbContext.SaveChanges();
            }

            // Act
            using (var dbContext = _fixture.CreateDbContext())
            {
                var productsToDelete = dbContext.Products.Where(p => p.Id % 2 == 0);
                dbContext.Products.RemoveRange(productsToDelete);
                dbContext.SaveChanges();
            }

            var expectedRemainingProducts = products.Where(p => p.Id % 2 != 0);

            // Assert
            using (var dbContext = _fixture.CreateDbContext())
            {
                var remainingProducts = dbContext.Products.ToList();
                remainingProducts.Should().HaveCount(5);
                remainingProducts.Should().BeEquivalentTo(expectedRemainingProducts, options => options.Excluding(o => o.Orders));
            }
        }

        [Fact]
        public void CanAddAndUpdateMultipleOrders()
        {
            // Arrange
            var customers = Enumerable.Range(1, 10)
                .Select(TestHelper.CreateCustomerEntity)
                .ToList();

            var products = Enumerable.Range(1, 10)
                .Select(TestHelper.CreateProductEntity)
                .ToList();

            var orders = new List<OrderEntity>();

            foreach (var customer in customers)
            {
                foreach (var product in products)
                {
                    var order = TestHelper.CreateOrderEntity(orders.Count + 1, customer.Id, product.Id);
                    orders.Add(order);
                }
            }

            using (var dbContext = _fixture.CreateDbContext())
            {
                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();

                dbContext.Customers.AddRange(customers);
                dbContext.Products.AddRange(products);
                dbContext.Orders.AddRange(orders);
                dbContext.SaveChanges();
            }

            // Act
            using (var dbContext = _fixture.CreateDbContext())
            {
                foreach (var order in orders)
                {
                    var orderToUpdate = dbContext.Orders.Single(o => o.Id == order.Id);
                    orderToUpdate.Status = (int)OrderStatus.Completed;
                }

                dbContext.SaveChanges();
            }

            // Assert
            using (var dbContext = _fixture.CreateDbContext())
            {
                foreach (var order in orders)
                {
                    var updatedOrder = dbContext.Orders.Single(o => o.Id == order.Id);
                    updatedOrder.Status.Should().Be((int)OrderStatus.Completed);
                }
            }
        }

        [Fact]
        public void CanDeleteOrdersAndCheckRemaining()
        {
            // Arrange
            var customers = Enumerable.Range(1, 10)
                .Select(TestHelper.CreateCustomerEntity)
                .ToList();

            var products = Enumerable.Range(1, 10)
                .Select(TestHelper.CreateProductEntity)
                .ToList();

            var orders = new List<OrderEntity>();

            foreach (var customer in customers)
            {
                foreach (var product in products)
                {
                    var order = TestHelper.CreateOrderEntity(orders.Count + 1, customer.Id, product.Id);
                    orders.Add(order);
                }
            }

            using (var dbContext = _fixture.CreateDbContext())
            {
                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();

                dbContext.Customers.AddRange(customers);
                dbContext.Products.AddRange(products);
                dbContext.Orders.AddRange(orders);
                dbContext.SaveChanges();
            }

            // Act
            using (var dbContext = _fixture.CreateDbContext())
            {
                var ordersToDelete = dbContext.Orders.Where(o => o.Id % 2 == 0);
                dbContext.Orders.RemoveRange(ordersToDelete);
                dbContext.SaveChanges();
            }

            var expectedRemainingOrders = orders.Where(o => o.Id % 2 != 0);

            // Assert
            using (var dbContext = _fixture.CreateDbContext())
            {
                var remainingOrders = dbContext.Orders.ToList();
                remainingOrders.Should().HaveCount(50);
                remainingOrders.Should().BeEquivalentTo(expectedRemainingOrders, options => 
                    options.Excluding(o => o.Product).Excluding(o => o.Customer));
            }
        }
    }
}