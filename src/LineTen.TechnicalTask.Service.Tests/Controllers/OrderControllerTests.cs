using AutoMapper;
using LineTen.TechnicalTask.Domain.Enums;
using LineTen.TechnicalTask.Domain.Models;
using LineTen.TechnicalTask.Service.Controllers;
using LineTen.TechnicalTask.Service.Domain.Mappings;
using LineTen.TechnicalTask.Service.Domain.Models;
using LineTen.TechnicalTask.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LineTen.TechnicalTask.Service.Tests.Controllers
{
    public class OrderControllerTests
    {
        private readonly OrderController _testClass;
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrderControllerTests()
        {
            _orderService = Substitute.For<IOrderService>();
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<RequestModelProfile>();
                cfg.AddProfile<ResponseModelProfile>();
            }).CreateMapper();

            _testClass = new OrderController(_orderService, _mapper);
        }

        [Fact]
        public void CanConstruct()
        {
            // Act
            var instance = new OrderController(_orderService, _mapper);

            // Assert
            instance.Should().NotBeNull();
        }

        [Fact]
        public void CannotConstructWithNullOrderService()
        {
            FluentActions.Invoking(() => new OrderController(default(IOrderService), _mapper)).Should().Throw<ArgumentNullException>().WithParameterName("orderService");
        }

        [Fact]
        public void CannotConstructWithNullMapper()
        {
            FluentActions.Invoking(() => new OrderController(_orderService, default(IMapper))).Should().Throw<ArgumentNullException>().WithParameterName("mapper");
        }

        [Fact]
        public async Task CanCallAddOrderAsync()
        {
            // Arrange
            var addOrderRequest = new AddOrderRequest
            {
                ProductId = 1,
                CustomerId = 2,
                Status = OrderStatus.New
            };

            var newOrder = new Order
            {
                Id = 1,
                ProductId = 1,
                CustomerId = 2,
                Status = OrderStatus.New
            };

            _orderService.AddOrderAsync(Arg.Any<Order>(), Arg.Any<CancellationToken>()).Returns(newOrder);

            // Act
            var actionResult = await _testClass.AddOrderAsync(addOrderRequest, CancellationToken.None) as OkObjectResult;
            var result = actionResult!.Value as OrderResponse;

            // Assert
            await _orderService.Received(1).AddOrderAsync(Arg.Any<Order>(), Arg.Any<CancellationToken>());

            result.Should().BeEquivalentTo(newOrder).And.BeEquivalentTo(addOrderRequest);
        }

        [Fact]
        public async Task CanCallGetAllOrdersAsync()
        {
            // Arrange
            var orders = Substitute.For<ICollection<Order>>();

            _orderService.GetAllOrdersAsync(Arg.Any<CancellationToken>())
                .Returns(orders);

            // Act
            var actionResult = await _testClass.GetAllOrdersAsync(CancellationToken.None) as OkObjectResult;
            var result = actionResult!.Value as ICollection<OrderResponse>;

            // Assert
            await _orderService.Received().GetAllOrdersAsync(Arg.Any<CancellationToken>());

            result.Should().BeEquivalentTo(orders);
        }

        [Fact]
        public async Task CanCallGetOrderAsync()
        {
            // Arrange
            var id = 388898791;
            var order = new Order
            {
                Id = 388898791,
                ProductId = 1,
                CustomerId = 2,
                Status = OrderStatus.InProgress
            };

            _orderService.GetOrderAsync(id, Arg.Any<CancellationToken>()).Returns(order);

            // Act
            var actionResult = await _testClass.GetOrderAsync(id, CancellationToken.None) as OkObjectResult;
            var result = actionResult!.Value as OrderResponse;

            // Assert
            await _orderService.Received().GetOrderAsync(id, Arg.Any<CancellationToken>());

            result.Should().BeEquivalentTo(order);
        }

        [Fact]
        public async Task CannotCallGetOrderAsyncWithInvalidId()
        {
            // Arrange
            var invalidId = default(int);

            // Act
            var actionResult = await _testClass.GetOrderAsync(invalidId, CancellationToken.None) as BadRequestObjectResult;

            // Assert
            actionResult.Should().NotBeNull();
            actionResult!.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        [Fact]
        public async Task CannotCallGetOrderAsyncWithNonexistentId()
        {
            // Arrange
            var nonExistentId = 1;

            _orderService.GetOrderAsync(nonExistentId, Arg.Any<CancellationToken>()).Returns(default(Order?));

            // Act
            var actionResult = await _testClass.GetOrderAsync(nonExistentId, CancellationToken.None) as NotFoundResult;

            // Assert
            actionResult.Should().NotBeNull();
            actionResult!.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }

        [Fact]
        public async Task CanCallUpdateOrderAsync()
        {
            // Arrange
            var updateOrderRequest = new UpdateOrderRequest
            {
                Id = 424208094,
                ProductId = 1,
                CustomerId = 2,
                Status = OrderStatus.PaymentReceived
            };

            var order = new Order
            {
                Id = 424208094,
                ProductId = 1,
                CustomerId = 2,
                Status = OrderStatus.PaymentReceived
            };

            var cancellationToken = CancellationToken.None;

            _orderService.UpdateOrderAsync(Arg.Any<Order>(), Arg.Any<CancellationToken>()).Returns(order);

            // Act
            var actionResult = await _testClass.UpdateOrderAsync(updateOrderRequest, cancellationToken) as OkObjectResult;
            var result = actionResult!.Value as OrderResponse;

            // Assert
            await _orderService.Received().UpdateOrderAsync(Arg.Any<Order>(), Arg.Any<CancellationToken>());

            result.Should().BeEquivalentTo(order).And.BeEquivalentTo(updateOrderRequest);
        }

        [Fact]
        public async Task CanCallDeleteOrderAsync()
        {
            // Arrange
            var id = 1000729004;
            var cancellationToken = CancellationToken.None;

            _orderService.DeleteOrderAsync(id, Arg.Any<CancellationToken>()).Returns(true);

            // Act
            var actionResult = await _testClass.DeleteOrderAsync(id, cancellationToken);

            // Assert
            await _orderService.Received(1).DeleteOrderAsync(id, Arg.Any<CancellationToken>());

            actionResult.Should().BeOfType<OkResult>();
        }

        [Fact]
        public async Task CannotCallDeleteOrderAsyncWithNonexistentId()
        {
            // Arrange
            var nonExistentId = 1;

            _orderService.DeleteOrderAsync(nonExistentId, Arg.Any<CancellationToken>()).Returns(false);

            // Act
            var actionResult = await _testClass.DeleteOrderAsync(nonExistentId, CancellationToken.None) as NotFoundResult;

            // Assert
            actionResult.Should().NotBeNull();
            actionResult!.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }
    }
}