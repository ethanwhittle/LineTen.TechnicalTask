using LineTen.TechnicalTask.Data.Repositories;
using LineTen.TechnicalTask.Domain.Enums;
using LineTen.TechnicalTask.Domain.Models;
using LineTen.TechnicalTask.Service.Services;

namespace LineTen.TechnicalTask.Service.Tests.Services
{
    public class OrderServiceTests
    {
        private readonly OrderService _testClass;
        private readonly IOrderRepository _orderRepository;

        public OrderServiceTests()
        {
            _orderRepository = Substitute.For<IOrderRepository>();
            _testClass = new OrderService(_orderRepository);
        }

        [Fact]
        public void CanConstruct()
        {
            // Act
            var instance = new OrderService(_orderRepository);

            // Assert
            instance.Should().NotBeNull();
        }

        [Fact]
        public void CannotConstructWithNullOrderRepository()
        {
            FluentActions.Invoking(() => new OrderService(default(IOrderRepository))).Should().Throw<ArgumentNullException>().WithParameterName("orderRepository");
        }

        [Fact]
        public async Task CanCallAddOrderAsync()
        {
            // Arrange
            var order = new Order
            {
                Id = 1067823901,
                ProductId = 1309000032,
                CustomerId = 373812170,
                Status = OrderStatus.New,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            };

            _orderRepository.AddOrderAsync(Arg.Any<Order>(), Arg.Any<CancellationToken>()).Returns(order);

            // Act
            var result = await _testClass.AddOrderAsync(order, CancellationToken.None);

            // Assert
            await _orderRepository.Received(1).AddOrderAsync(Arg.Any<Order>(), Arg.Any<CancellationToken>());

            result.Should().BeEquivalentTo(order);
        }

        [Fact]
        public async Task CannotCallAddOrderAsyncWithNullNewOrder()
        {
            await FluentActions.Invoking(() => _testClass.AddOrderAsync(default(Order), CancellationToken.None)).Should().ThrowAsync<ArgumentNullException>().WithParameterName("newOrder");
        }

        [Fact]
        public async Task CanCallDeleteOrderAsync()
        {
            // Arrange
            var id = 1778902675;

            _orderRepository.DeleteOrderAsync(id, Arg.Any<CancellationToken>()).Returns(true);

            // Act
            var result = await _testClass.DeleteOrderAsync(id, CancellationToken.None);

            // Assert
            await _orderRepository.Received(1).DeleteOrderAsync(id, Arg.Any<CancellationToken>());

            result.Should().BeTrue();
        }

        [Fact]
        public async Task CanCallGetAllOrdersAsync()
        {
            // Arrange
            var orders = Substitute.For<ICollection<Order>>();

            _orderRepository.GetAllOrdersAsync(Arg.Any<CancellationToken>()).Returns(orders);

            // Act
            var result = await _testClass.GetAllOrdersAsync(CancellationToken.None);

            // Assert
            await _orderRepository.Received(1).GetAllOrdersAsync(Arg.Any<CancellationToken>());

            result.Should().BeEquivalentTo(orders);
        }

        [Fact]
        public async Task CanCallGetOrderAsync()
        {
            // Arrange
            var id = 425218925;

            var order = new Order
            {
                Id = 425218925,
                ProductId = 1944172174,
                CustomerId = 1175589849,
                Status = OrderStatus.PaymentFailed,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            };

            _orderRepository.GetOrderAsync(id, Arg.Any<CancellationToken>()).Returns(order);

            // Act
            var result = await _testClass.GetOrderAsync(id, CancellationToken.None);

            // Assert
            await _orderRepository.Received(1).GetOrderAsync(id, Arg.Any<CancellationToken>());

            result.Should().BeEquivalentTo(order);
        }

        [Fact]
        public async Task CanCallUpdateOrderAsync()
        {
            // Arrange
            var updatedOrder = new Order
            {
                Id = 991212302,
                ProductId = 1980327344,
                CustomerId = 1484426704,
                Status = OrderStatus.Closed,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            };

            _orderRepository.UpdateOrderAsync(updatedOrder, Arg.Any<CancellationToken>()).Returns(updatedOrder);

            // Act
            var result = await _testClass.UpdateOrderAsync(updatedOrder, CancellationToken.None);

            // Assert
            await _orderRepository.Received(1).UpdateOrderAsync(Arg.Any<Order>(), Arg.Any<CancellationToken>());

            result.Should().BeEquivalentTo(updatedOrder);
        }

        [Fact]
        public async Task CannotCallUpdateOrderAsyncWithNullUpdatedOrder()
        {
            await FluentActions.Invoking(() => _testClass.UpdateOrderAsync(default(Order), CancellationToken.None)).Should().ThrowAsync<ArgumentNullException>().WithParameterName("updatedOrder");
        }

        [Fact]
        public async Task CannotCallUpdateOrderAsyncWithDefaultId()
        {
            // Arrange
            var invalidOrder = new Order
            {
                Id = default,
                ProductId = 1,
                CustomerId = 1,
                Status = OrderStatus.Completed
            };

            // Assert
            await FluentActions.Invoking(() => _testClass.UpdateOrderAsync(invalidOrder, CancellationToken.None)).Should().ThrowAsync<ArgumentOutOfRangeException>().WithParameterName("updatedOrder.Id");
        }
    }
}