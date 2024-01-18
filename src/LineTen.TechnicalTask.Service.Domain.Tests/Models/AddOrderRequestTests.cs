using LineTen.TechnicalTask.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace LineTen.TechnicalTask.Service.Domain.Tests.Models
{
    public class AddOrderRequest
    {
        [Required(ErrorMessage = "ProductId is required.")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "CustomerId is required.")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        public OrderStatus Status { get; set; }
    }

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