namespace LineTen.TechnicalTask.Domain.Tests.Models
{
    public class OrderTests
    {
        public enum OrderStatus : int
        {
            None = 0,
            New = 1,
            PaymentReceived = 2,
            PaymentFailed = 3,
            InProgress = 4,
            Completed = 5,
            Closed = 6,
            Cancelled = 7
        }

        public class Order
        {
            public int ProductId { get; init; }

            public int CustomerId { get; init; }

            public OrderStatus Status { get; init; }

            public DateTime CreatedDate { get; init; }

            public DateTime UpdatedDate { get; init; }
        }

        private readonly Order _testClass;
        private readonly int _productId;
        private readonly int _customerId;
        private readonly OrderStatus _status;
        private readonly DateTime _createdDate;
        private readonly DateTime _updatedDate;

        public OrderTests()
        {
            _productId = 2066213427;
            _customerId = 892124983;
            _status = OrderStatus.PaymentReceived;
            _createdDate = DateTime.UtcNow;
            _updatedDate = DateTime.UtcNow;
            _testClass = new Order
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
            var instance = new Order();

            // Assert
            instance.Should().NotBeNull();
        }

        [Fact]
        public void CanInitialize()
        {
            // Act
            var instance = new Order
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