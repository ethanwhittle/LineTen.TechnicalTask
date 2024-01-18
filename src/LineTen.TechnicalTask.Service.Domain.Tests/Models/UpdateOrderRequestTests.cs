using LineTen.TechnicalTask.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace LineTen.TechnicalTask.Service.Domain.Tests.Models
{
    public class UpdateOrderRequest
    {
        [Required(ErrorMessage = "Id is required.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "ProductId is required.")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "CustomerId is required.")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        public OrderStatus Status { get; set; }
    }

    public class UpdateOrderRequestTests
    {
        private readonly UpdateOrderRequest _testClass;

        public UpdateOrderRequestTests()
        {
            _testClass = new UpdateOrderRequest();
        }

        [Fact]
        public void CanSetAndGetId()
        {
            // Arrange
            var testValue = 453311664;

            // Act
            _testClass.Id = testValue;

            // Assert
            _testClass.Id.Should().Be(testValue);
        }

        [Fact]
        public void CanSetAndGetProductId()
        {
            // Arrange
            var testValue = 477235169;

            // Act
            _testClass.ProductId = testValue;

            // Assert
            _testClass.ProductId.Should().Be(testValue);
        }

        [Fact]
        public void CanSetAndGetCustomerId()
        {
            // Arrange
            var testValue = 514267070;

            // Act
            _testClass.CustomerId = testValue;

            // Assert
            _testClass.CustomerId.Should().Be(testValue);
        }

        [Fact]
        public void CanSetAndGetStatus()
        {
            // Arrange
            var testValue = OrderStatus.None;

            // Act
            _testClass.Status = testValue;

            // Assert
            _testClass.Status.Should().Be(testValue);
        }
    }
}