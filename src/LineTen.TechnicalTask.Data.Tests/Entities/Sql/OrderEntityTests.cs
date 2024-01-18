using LineTen.TechnicalTask.Data.Entities.Sql;

namespace LineTen.TechnicalTask.Data.Tests.Entities.Sql
{
    public class OrderEntityTests
    {
        private readonly OrderEntity _testClass;

        public OrderEntityTests()
        {
            _testClass = new OrderEntity();
        }

        [Fact]
        public void CanSetAndGetId()
        {
            // Arrange
            var testValue = 372271463;

            // Act
            _testClass.Id = testValue;

            // Assert
            _testClass.Id.Should().Be(testValue);
        }

        [Fact]
        public void CanSetAndGetProductId()
        {
            // Arrange
            var testValue = 1225466355;

            // Act
            _testClass.ProductId = testValue;

            // Assert
            _testClass.ProductId.Should().Be(testValue);
        }

        [Fact]
        public void CanSetAndGetCustomerId()
        {
            // Arrange
            var testValue = 1773680417;

            // Act
            _testClass.CustomerId = testValue;

            // Assert
            _testClass.CustomerId.Should().Be(testValue);
        }

        [Fact]
        public void CanSetAndGetStatus()
        {
            // Arrange
            var testValue = 549139642;

            // Act
            _testClass.Status = testValue;

            // Assert
            _testClass.Status.Should().Be(testValue);
        }

        [Fact]
        public void CanSetAndGetCreatedDate()
        {
            // Arrange
            var testValue = DateTime.UtcNow;

            // Act
            _testClass.CreatedDate = testValue;

            // Assert
            _testClass.CreatedDate.Should().Be(testValue);
        }

        [Fact]
        public void CanSetAndGetUpdatedDate()
        {
            // Arrange
            var testValue = DateTime.UtcNow;

            // Act
            _testClass.UpdatedDate = testValue;

            // Assert
            _testClass.UpdatedDate.Should().Be(testValue);
        }

        [Fact]
        public void CanSetAndGetProduct()
        {
            // Arrange
            var testValue = new ProductEntity
            {
                Id = 711537661,
                Name = "TestValue517796829",
                Description = "TestValue1041068545",
                SKU = "TestValue1083280915",
                Orders = Substitute.For<ICollection<OrderEntity>>()
            };

            // Act
            _testClass.Product = testValue;

            // Assert
            _testClass.Product.Should().BeSameAs(testValue);
        }

        [Fact]
        public void CanSetAndGetCustomer()
        {
            // Arrange
            var testValue = new CustomerEntity
            {
                Id = 1982299221,
                FirstName = "TestValue609958224",
                LastName = "TestValue1512010021",
                Phone = "TestValue1852522702",
                Email = "TestValue1102817101",
                Orders = Substitute.For<ICollection<OrderEntity>>()
            };

            // Act
            _testClass.Customer = testValue;

            // Assert
            _testClass.Customer.Should().BeSameAs(testValue);
        }
    }
}