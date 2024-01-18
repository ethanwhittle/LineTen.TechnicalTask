using LineTen.TechnicalTask.Data.Entities.Sql;

namespace LineTen.TechnicalTask.Data.Tests.Entities.Sql
{
    public class ProductEntityTests
    {
        private readonly ProductEntity _testClass;

        public ProductEntityTests()
        {
            _testClass = new ProductEntity();
        }

        [Fact]
        public void CanSetAndGetId()
        {
            // Arrange
            var testValue = 1569922024;

            // Act
            _testClass.Id = testValue;

            // Assert
            _testClass.Id.Should().Be(testValue);
        }

        [Fact]
        public void CanSetAndGetName()
        {
            // Arrange
            var testValue = "TestValue416920284";

            // Act
            _testClass.Name = testValue;

            // Assert
            _testClass.Name.Should().Be(testValue);
        }

        [Fact]
        public void CanSetAndGetDescription()
        {
            // Arrange
            var testValue = "TestValue848908530";

            // Act
            _testClass.Description = testValue;

            // Assert
            _testClass.Description.Should().Be(testValue);
        }

        [Fact]
        public void CanSetAndGetSKU()
        {
            // Arrange
            var testValue = "TestValue2111957761";

            // Act
            _testClass.SKU = testValue;

            // Assert
            _testClass.SKU.Should().Be(testValue);
        }

        [Fact]
        public void CanSetAndGetOrders()
        {
            // Arrange
            var testValue = Substitute.For<ICollection<OrderEntity>>();

            // Act
            _testClass.Orders = testValue;

            // Assert
            _testClass.Orders.Should().BeSameAs(testValue);
        }
    }
}