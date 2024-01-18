using LineTen.TechnicalTask.Domain.Enums;
using LineTen.TechnicalTask.Service.Domain.Models;

namespace LineTen.TechnicalTask.Service.Domain.Tests.Models
{
    public class OrderResponseTests
    {
        private readonly OrderResponse _testClass;

        public OrderResponseTests()
        {
            _testClass = new OrderResponse();
        }

        [Fact]
        public void CanSetAndGetId()
        {
            // Arrange
            var testValue = 1383607004;

            // Act
            _testClass.Id = testValue;

            // Assert
            _testClass.Id.Should().Be(testValue);
        }

        [Fact]
        public void CanSetAndGetProductId()
        {
            // Arrange
            var testValue = 2091606174;

            // Act
            _testClass.ProductId = testValue;

            // Assert
            _testClass.ProductId.Should().Be(testValue);
        }

        [Fact]
        public void CanSetAndGetCustomerId()
        {
            // Arrange
            var testValue = 2083707697;

            // Act
            _testClass.CustomerId = testValue;

            // Assert
            _testClass.CustomerId.Should().Be(testValue);
        }

        [Fact]
        public void CanSetAndGetStatus()
        {
            // Arrange
            var testValue = OrderStatus.PaymentReceived;

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
    }
}