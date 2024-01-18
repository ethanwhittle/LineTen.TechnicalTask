using System.ComponentModel.DataAnnotations;

namespace LineTen.TechnicalTask.Service.Domain.Tests.Models
{
    public class AddProductRequest
    {
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = "SKU is required.")]
        public string SKU { get; set; } = null!;
    }

    public class AddProductRequestTests
    {
        private readonly AddProductRequest _testClass;

        public AddProductRequestTests()
        {
            _testClass = new AddProductRequest();
        }

        [Fact]
        public void CanSetAndGetName()
        {
            // Arrange
            var testValue = "TestValue1297858634";

            // Act
            _testClass.Name = testValue;

            // Assert
            _testClass.Name.Should().Be(testValue);
        }

        [Fact]
        public void CanSetAndGetDescription()
        {
            // Arrange
            var testValue = "TestValue345967755";

            // Act
            _testClass.Description = testValue;

            // Assert
            _testClass.Description.Should().Be(testValue);
        }

        [Fact]
        public void CanSetAndGetSKU()
        {
            // Arrange
            var testValue = "TestValue1630041401";

            // Act
            _testClass.SKU = testValue;

            // Assert
            _testClass.SKU.Should().Be(testValue);
        }
    }
}