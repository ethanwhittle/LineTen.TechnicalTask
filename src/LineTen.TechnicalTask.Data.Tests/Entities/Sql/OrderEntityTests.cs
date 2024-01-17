namespace LineTen.TechnicalTask.Data.Tests.Entities.Sql
{
    public class OrderEntityTests
    {
        public class OrderEntity
        {
            // TODO: Navigation properties

            public int ProductId { get; init; }

            public int CustomerId { get; init; }

            public int Status { get; init; }

            public DateTime CreatedDate { get; init; }

            public DateTime UpdatedDate { get; init; }
        }

        private readonly OrderEntity _testClass;
        private readonly int _productId;
        private readonly int _customerId;
        private readonly int _status;
        private readonly DateTime _createdDate;
        private readonly DateTime _updatedDate;

        public OrderEntityTests()
        {
            _productId = 565426790;
            _customerId = 2022302003;
            _status = 1069059804;
            _createdDate = DateTime.UtcNow;
            _updatedDate = DateTime.UtcNow;
            _testClass = new OrderEntity
            {
                ProductId = _productId,
                CustomerId = _customerId,
                Status = _status,
                CreatedDate = _createdDate,
                UpdatedDate = _updatedDate
            };
        }

        [Fact]
        public void CanConstruct()
        {
            // Act
            var instance = new OrderEntity();

            // Assert
            instance.Should().NotBeNull();
        }

        [Fact]
        public void CanInitialize()
        {
            // Act
            var instance = new OrderEntity
            {
                ProductId = _productId,
                CustomerId = _customerId,
                Status = _status,
                CreatedDate = _createdDate,
                UpdatedDate = _updatedDate
            };

            // Assert
            instance.Should().NotBeNull();
        }

        [Fact]
        public void ProductIdIsInitializedCorrectly()
        {
            _testClass.ProductId.Should().Be(_productId);
        }

        [Fact]
        public void CustomerIdIsInitializedCorrectly()
        {
            _testClass.CustomerId.Should().Be(_customerId);
        }

        [Fact]
        public void StatusIsInitializedCorrectly()
        {
            _testClass.Status.Should().Be(_status);
        }

        [Fact]
        public void CreatedDateIsInitializedCorrectly()
        {
            _testClass.CreatedDate.Should().Be(_createdDate);
        }

        [Fact]
        public void UpdatedDateIsInitializedCorrectly()
        {
            _testClass.UpdatedDate.Should().Be(_updatedDate);
        }
    }
}