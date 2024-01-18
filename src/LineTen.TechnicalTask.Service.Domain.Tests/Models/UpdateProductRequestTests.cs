using System.ComponentModel.DataAnnotations;

namespace LineTen.TechnicalTask.Service.Domain.Tests.Models
{
    public class UpdateProductRequest
    {
        [Required(ErrorMessage = "Id is required.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = "SKU is required.")]
        public string SKU { get; set; } = null!;
    }

    public class UpdateProductRequestTests
    {
        private readonly UpdateProductRequest _testClass;

        public UpdateProductRequestTests()
        {
            _testClass = new UpdateProductRequest();
        }

        [Fact]
        public void CanSetAndGetId()
        {
            // Arrange
            var testValue = 412921620;

            // Act
            _testClass.Id = testValue;

            // Assert
            _testClass.Id.Should().Be(testValue);
        }

        [Fact]
        public void CanSetAndGetName()
        {
            // Arrange
            var testValue = "TestValue2010406517";

            // Act
            _testClass.Name = testValue;

            // Assert
            _testClass.Name.Should().Be(testValue);
        }

        [Fact]
        public void CanSetAndGetDescription()
        {
            // Arrange
            var testValue = "TestValue1664947177";

            // Act
            _testClass.Description = testValue;

            // Assert
            _testClass.Description.Should().Be(testValue);
        }

        [Fact]
        public void CanSetAndGetSKU()
        {
            // Arrange
            var testValue = "TestValue187967903";

            // Act
            _testClass.SKU = testValue;

            // Assert
            _testClass.SKU.Should().Be(testValue);
        }
    }
}