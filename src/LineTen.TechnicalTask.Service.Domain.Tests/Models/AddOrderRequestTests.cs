using LineTen.TechnicalTask.Domain.Enums;
using LineTen.TechnicalTask.Service.Domain.Models;

namespace LineTen.TechnicalTask.Service.Domain.Tests.Models
{
    public class AddOrderRequestTests
    {
        private readonly AddOrderRequest _testClass;

        public AddOrderRequestTests()
        {
            _testClass = new AddOrderRequest();
        }

        [Fact]
        public void CanSetAndGetProductId()
        {
            // Arrange
            var testValue = 324483756;

            // Act
            _testClass.ProductId = testValue;

            // Assert
            _testClass.ProductId.Should().Be(testValue);
        }

        [Fact]
        public void CanSetAndGetCustomerId()
        {
            // Arrange
            var testValue = 1439042814;

            // Act
            _testClass.CustomerId = testValue;

            // Assert
            _testClass.CustomerId.Should().Be(testValue);
        }

        [Fact]
        public void CanSetAndGetStatus()
        {
            // Arrange
            var testValue = OrderStatus.New;

            // Act
            _testClass.Status = testValue;

            // Assert
            _testClass.Status.Should().Be(testValue);
        }
    }
}