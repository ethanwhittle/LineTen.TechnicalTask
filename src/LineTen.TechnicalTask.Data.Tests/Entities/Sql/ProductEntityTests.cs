using System.ComponentModel.DataAnnotations;

namespace LineTen.TechnicalTask.Data.Tests.Entities.Sql
{
    public class ProductEntityTests
    {
        public class ProductEntity
        {
            // TODO: Navigation property

            [Key]
            public int Id { get; init; }

            public string Name { get; init; } = null!;

            public string Description { get; init; } = null!;

            public string SKU { get; init; } = null!;
        }

        private readonly ProductEntity _testClass;
        private readonly int _id;
        private readonly string _name;
        private readonly string _description;
        private readonly string _sKU;

        public ProductEntityTests()
        {
            _id = 408485393;
            _name = "TestValue1682786041";
            _description = "TestValue1512516772";
            _sKU = "TestValue1750663484";
            _testClass = new ProductEntity
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
            var instance = new ProductEntity();

            // Assert
            instance.Should().NotBeNull();
        }

        [Fact]
        public void CanInitialize()
        {
            // Act
            var instance = new ProductEntity
            {
                Id = _id,
                Name = _name,
                Description = _description,
                SKU = _sKU
            };

            // Assert
            instance.Should().NotBeNull();
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