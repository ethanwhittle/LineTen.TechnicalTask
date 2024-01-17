using LineTen.TechnicalTask.Domain.Models;

namespace LineTen.TechnicalTask.Domain.Tests.Models
{
    public class ProductTests
    {
        private readonly Product _testClass;
        private readonly int _id;
        private readonly string _name;
        private readonly string _description;
        private readonly string _sKU;

        public ProductTests()
        {
            _id = 363387035;
            _name = "TestValue805651740";
            _description = "TestValue710094062";
            _sKU = "TestValue251737194";
            _testClass = new Product
            {
                Id = _id,
                Name = _name,
                Description = _description,
                SKU = _sKU
            };
        }

        [Fact]
        public void CanConstruct()
        {
            // Act
            var instance = new Product();

            // Assert
            instance.Should().NotBeNull();
        }

        [Fact]
        public void CanInitialize()
        {
            // Act
            var instance = new Product
            {
                Id = _id,
                Name = _name,
                Description = _description,
                SKU = _sKU
            };

            // Assert
            instance.Should().NotBeNull();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void CannotInitializeWithInvalidName(string value)
        {
            FluentActions.Invoking(() => new Product
            {
                Id = _id,
                Name = value,
                Description = _description,
                SKU = _sKU
            }).Should().Throw<ArgumentNullException>().WithParameterName("Name");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void CannotInitializeWithInvalidDescription(string value)
        {
            FluentActions.Invoking(() => new Product
            {
                Id = _id,
                Name = _name,
                Description = value,
                SKU = _sKU
            }).Should().Throw<ArgumentNullException>().WithParameterName("Description");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void CannotInitializeWithInvalidSKU(string value)
        {
            FluentActions.Invoking(() => new Product
            {
                Id = _id,
                Name = _name,
                Description = _description,
                SKU = value
            }).Should().Throw<ArgumentNullException>().WithParameterName("SKU");
        }

        [Fact]
        public void IdIsInitializedCorrectly()
        {
            _testClass.Id.Should().Be(_id);
        }

        [Fact]
        public void NameIsInitializedCorrectly()
        {
            _testClass.Name.Should().Be(_name);
        }

        [Fact]
        public void DescriptionIsInitializedCorrectly()
        {
            _testClass.Description.Should().Be(_description);
        }

        [Fact]
        public void SKUIsInitializedCorrectly()
        {
            _testClass.SKU.Should().Be(_sKU);
        }
    }
}