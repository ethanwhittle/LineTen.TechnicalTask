using LineTen.TechnicalTask.Domain.Enums;
using LineTen.TechnicalTask.Service.Domain.Models;
using LineTen.TechnicalTask.Service.IntegrationTests.Core;
using System.Net;
using System.Net.Http.Json;

namespace LineTen.TechnicalTask.Service.IntegrationTests.Controllers
{
    [CollectionDefinition(nameof(IntegrationTestCollection))]
    public class OrderControllerTests : IClassFixture<IntegrationTestContext>
    {
        private readonly IntegrationTestContext _context;

        public OrderControllerTests(IntegrationTestContext context)
        {
            _context = context;

            // Ensure a clean state for each test case
            _context.DbContext.Database.EnsureDeleted();
            _context.DbContext.Database.EnsureCreated();
        }

        [Fact]
        public async Task AddOrderAsync_ValidRequest_ReturnsOkResult()
        {
            // Arrange
            var addProductRequest = new AddProductRequest
            {
                Name = "Product1",
                Description = "Description1",
                SKU = "SKU1"
            };

            var addProductResponse = await _context.HttpClient.PostAsJsonAsync("/api/products", addProductRequest);
            addProductResponse.EnsureSuccessStatusCode();
            var addedProduct = await addProductResponse.Content.ReadFromJsonAsync<ProductResponse>();

            var addCustomerRequest = new AddCustomerRequest
            {
                FirstName = "Ethan",
                LastName = "Whittle",
                Phone = "+447525417714",
                Email = "ethanwhittle@hotmail.co.uk"
            };

            var addCustomerResponse = await _context.HttpClient.PostAsJsonAsync("/api/customers", addCustomerRequest);
            addCustomerResponse.EnsureSuccessStatusCode();
            var addedCustomer = await addCustomerResponse.Content.ReadFromJsonAsync<CustomerResponse>();

            var addOrderRequest = new AddOrderRequest
            {
                ProductId = addedProduct!.Id,
                CustomerId = addedCustomer!.Id,
                Status = OrderStatus.New
            };

            // Act
            var response = await _context.HttpClient.PostAsJsonAsync("/api/orders", addOrderRequest);
            response.EnsureSuccessStatusCode();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var result = await response.Content.ReadFromJsonAsync<OrderResponse>();
            result.Should().NotBeNull();
            result!.ProductId.Should().Be(addOrderRequest.ProductId);
            result!.CustomerId.Should().Be(addOrderRequest.CustomerId);
            result!.Status.Should().Be(addOrderRequest.Status);
        }

        [Fact]
        public async Task AddOrderAsync_InvalidRequest_ReturnsBadRequest()
        {
            // Arrange
            var addOrderRequest = new AddOrderRequest
            {
                Status = (OrderStatus)9000
            };

            // Act
            var response = await _context.HttpClient.PostAsJsonAsync("/api/orders", addOrderRequest);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task GetAllOrdersAsync_ReturnsOkResult()
        {
            // Act
            var response = await _context.HttpClient.GetAsync("/api/orders");
            response.EnsureSuccessStatusCode();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var result = await response.Content.ReadFromJsonAsync<List<OrderResponse>>();
            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }

        [Fact]
        public async Task GetOrderAsync_ExistingId_ReturnsOkResult()
        {
            // Arrange
            var addProductRequest = new AddProductRequest
            {
                Name = "Product1",
                Description = "Description1",
                SKU = "SKU1"
            };

            var addProductResponse = await _context.HttpClient.PostAsJsonAsync("/api/products", addProductRequest);
            addProductResponse.EnsureSuccessStatusCode();
            var addedProduct = await addProductResponse.Content.ReadFromJsonAsync<ProductResponse>();

            var addCustomerRequest = new AddCustomerRequest
            {
                FirstName = "Ethan",
                LastName = "Whittle",
                Phone = "+447525417714",
                Email = "ethanwhittle@hotmail.co.uk"
            };

            var addCustomerResponse = await _context.HttpClient.PostAsJsonAsync("/api/customers", addCustomerRequest);
            addCustomerResponse.EnsureSuccessStatusCode();
            var addedCustomer = await addCustomerResponse.Content.ReadFromJsonAsync<CustomerResponse>();

            var addOrderRequest = new AddOrderRequest
            {
                ProductId = addedProduct!.Id,
                CustomerId = addedCustomer!.Id,
                Status = OrderStatus.New
            };

            var addOrderResponse = await _context.HttpClient.PostAsJsonAsync("/api/orders", addOrderRequest);
            addOrderResponse.EnsureSuccessStatusCode();
            var addedOrder = await addOrderResponse.Content.ReadFromJsonAsync<OrderResponse>();

            addedOrder.Should().NotBeNull();

            var getOrderResponse = await _context.HttpClient.GetAsync($"/api/orders/{addedOrder!.Id}");
            getOrderResponse.EnsureSuccessStatusCode();

            // Assert
            getOrderResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var result = await getOrderResponse.Content.ReadFromJsonAsync<OrderResponse>();
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(addedOrder);
        }

        [Fact]
        public async Task GetOrderAsync_InvalidId_ReturnsBadRequest()
        {
            // Arrange
            var invalidOrderId = 0;

            // Act
            var response = await _context.HttpClient.GetAsync($"/api/orders/{invalidOrderId}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task GetOrderAsync_NonexistentId_ReturnsNotFound()
        {
            // Arrange
            var nonexistentOrderId = 1;

            // Act
            var response = await _context.HttpClient.GetAsync($"/api/orders/{nonexistentOrderId}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task UpdateOrderAsync_ValidRequest_ReturnsOkResult()
        {
            // Arrange
            var addProductRequest = new AddProductRequest
            {
                Name = "Product1",
                Description = "Description1",
                SKU = "SKU1"
            };

            var addProductResponse = await _context.HttpClient.PostAsJsonAsync("/api/products", addProductRequest);
            addProductResponse.EnsureSuccessStatusCode();
            var addedProduct = await addProductResponse.Content.ReadFromJsonAsync<ProductResponse>();

            var addCustomerRequest = new AddCustomerRequest
            {
                FirstName = "Ethan",
                LastName = "Whittle",
                Phone = "+447525417714",
                Email = "ethanwhittle@hotmail.co.uk"
            };

            var addCustomerResponse = await _context.HttpClient.PostAsJsonAsync("/api/customers", addCustomerRequest);
            addCustomerResponse.EnsureSuccessStatusCode();
            var addedCustomer = await addCustomerResponse.Content.ReadFromJsonAsync<CustomerResponse>();

            var addOrderRequest = new AddOrderRequest
            {
                ProductId = addedProduct!.Id,
                CustomerId = addedCustomer!.Id,
                Status = OrderStatus.New
            };

            var addOrderResponse = await _context.HttpClient.PostAsJsonAsync("/api/orders", addOrderRequest);
            addOrderResponse.EnsureSuccessStatusCode();
            var addedOrder = await addOrderResponse.Content.ReadFromJsonAsync<OrderResponse>();

            addedOrder.Should().NotBeNull();

            var updateOrderRequest = new UpdateOrderRequest
            {
                Id = addedOrder!.Id,
                ProductId = addedProduct.Id,
                CustomerId = addedCustomer.Id,
                Status = OrderStatus.Completed
            };

            // Act
            var updateOrderResponse = await _context.HttpClient.PutAsJsonAsync("/api/orders", updateOrderRequest);
            updateOrderResponse.EnsureSuccessStatusCode();

            var updatedOrder = await updateOrderResponse.Content.ReadFromJsonAsync<OrderResponse>();

            // Assert
            updateOrderResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            updatedOrder.Should().NotBeNull().And.BeEquivalentTo(updateOrderRequest);
        }

        [Fact]
        public async Task UpdateOrderAsync_InvalidRequest_ReturnsBadRequest()
        {
            // Arrange
            var updateOrderRequest = new UpdateOrderRequest();

            // Act
            var response = await _context.HttpClient.PutAsJsonAsync("/api/orders", updateOrderRequest);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task DeleteOrderAsync_ExistingId_ReturnsOkResult()
        {
            // Arrange
            var addProductRequest = new AddProductRequest
            {
                Name = "Product1",
                Description = "Description1",
                SKU = "SKU1"
            };

            var addProductResponse = await _context.HttpClient.PostAsJsonAsync("/api/products", addProductRequest);
            addProductResponse.EnsureSuccessStatusCode();
            var addedProduct = await addProductResponse.Content.ReadFromJsonAsync<ProductResponse>();

            var addCustomerRequest = new AddCustomerRequest
            {
                FirstName = "Ethan",
                LastName = "Whittle",
                Phone = "+447525417714",
                Email = "ethanwhittle@hotmail.co.uk"
            };

            var addCustomerResponse = await _context.HttpClient.PostAsJsonAsync("/api/customers", addCustomerRequest);
            addCustomerResponse.EnsureSuccessStatusCode();
            var addedCustomer = await addCustomerResponse.Content.ReadFromJsonAsync<CustomerResponse>();

            var addOrderRequest = new AddOrderRequest
            {
                ProductId = addedProduct!.Id,
                CustomerId = addedCustomer!.Id,
                Status = OrderStatus.New
            };

            var addOrderResponse = await _context.HttpClient.PostAsJsonAsync("/api/orders", addOrderRequest);
            addOrderResponse.EnsureSuccessStatusCode();
            var addedOrder = await addOrderResponse.Content.ReadFromJsonAsync<OrderResponse>();

            addedOrder.Should().NotBeNull();

            var deleteOrderResponse = await _context.HttpClient.DeleteAsync($"/api/orders/{addedOrder!.Id}");
            deleteOrderResponse.EnsureSuccessStatusCode();

            // Assert
            deleteOrderResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task DeleteOrderAsync_NonexistentId_ReturnsNotFound()
        {
            // Arrange
            var nonexistentOrderId = 1;

            // Act
            var response = await _context.HttpClient.DeleteAsync($"/api/orders/{nonexistentOrderId}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}