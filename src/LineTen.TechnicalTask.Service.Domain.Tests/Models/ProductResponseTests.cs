namespace LineTen.TechnicalTask.Service.Domain.Tests.Models
{
    public class ProductResponse
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string SKU { get; set; } = null!;
    }

    public class ProductResponseTests
    {
        private readonly ProductResponse _testClass;

        public ProductResponseTests()
        {
            _testClass = new ProductResponse();
        }

        [Fact]
        public void CanSetAndGetId()
        {
            // Arrange
            var testValue = 1324022851;

            // Act
            _testClass.Id = testValue;

            // Assert
            _testClass.Id.Should().Be(testValue);
        }

        [Fact]
        public void CanSetAndGetName()
        {
            // Arrange
            var testValue = "TestValue529731449";

            // Act
            _testClass.Name = testValue;

            // Assert
            _testClass.Name.Should().Be(testValue);
        }

        [Fact]
        public void CanSetAndGetDescription()
        {
            // Arrange
            var testValue = "TestValue78667494";

            // Act
            _testClass.Description = testValue;

            // Assert
            _testClass.Description.Should().Be(testValue);
        }

        [Fact]
        public void CanSetAndGetSKU()
        {
            // Arrange
            var testValue = "TestValue1044346411";

            // Act
            _testClass.SKU = testValue;

            // Assert
            _testClass.SKU.Should().Be(testValue);
        }
    }
}