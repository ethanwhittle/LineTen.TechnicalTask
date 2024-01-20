using LineTen.TechnicalTask.Service.Domain.Models;
using LineTen.TechnicalTask.Service.IntegrationTests.Core;
using System.Net;
using System.Net.Http.Json;

namespace LineTen.TechnicalTask.Service.IntegrationTests.Controllers
{
    [CollectionDefinition(nameof(IntegrationTestCollection))]
    public class ProductControllerTests : IClassFixture<IntegrationTestContext>
    {
        private readonly IntegrationTestContext _context;

        public ProductControllerTests(IntegrationTestContext context)
        {
            _context = context;

            // Ensure a clean state for each test case
            _context.DbContext.Database.EnsureDeleted();
            _context.DbContext.Database.EnsureCreated();
        }

        [Fact]
        public async Task AddProductAsync_ValidRequest_ReturnsOkResult()
        {
            // Arrange
            var addProductRequest = new AddProductRequest
            {
                Name = "Product1",
                Description = "Description1",
                SKU = "SKU1"
            };

            // Act
            var response = await _context.HttpClient.PostAsJsonAsync("/api/products", addProductRequest);
            response.EnsureSuccessStatusCode();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var result = await response.Content.ReadFromJsonAsync<ProductResponse>();
            result.Should().NotBeNull();
            result!.Name.Should().Be(addProductRequest.Name);
            result!.Description.Should().Be(addProductRequest.Description);
            result!.SKU.Should().Be(addProductRequest.SKU);
        }

        [Fact]
        public async Task AddProductAsync_InvalidRequest_ReturnsBadRequest()
        {
            // Arrange
            var addProductRequest = new AddProductRequest();

            // Act
            var response = await _context.HttpClient.PostAsJsonAsync("/api/products", addProductRequest);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task GetAllProductsAsync_ReturnsOkResult()
        {
            // Act
            var response = await _context.HttpClient.GetAsync("/api/products");
            response.EnsureSuccessStatusCode();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var result = await response.Content.ReadFromJsonAsync<List<ProductResponse>>();
            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }

        [Fact]
        public async Task GetProductAsync_ExistingId_ReturnsOkResult()
        {
            // Arrange
            var addProductRequest = new AddProductRequest
            {
                Name = "Product1",
                Description = "Description1",
                SKU = "SKU1"
            };

            // Act
            var addProductResponse = await _context.HttpClient.PostAsJsonAsync("/api/products", addProductRequest);
            addProductResponse.EnsureSuccessStatusCode();

            var addedProduct = await addProductResponse.Content.ReadFromJsonAsync<ProductResponse>();

            addedProduct.Should().NotBeNull();

            var getProductResponse = await _context.HttpClient.GetAsync($"/api/products/{addedProduct!.Id}");
            getProductResponse.EnsureSuccessStatusCode();

            // Assert
            getProductResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var result = await getProductResponse.Content.ReadFromJsonAsync<ProductResponse>();
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(addedProduct);
        }

        [Fact]
        public async Task GetProductAsync_InvalidId_ReturnsBadRequest()
        {
            // Arrange
            var invalidProductId = 0;

            // Act
            var response = await _context.HttpClient.GetAsync($"/api/products/{invalidProductId}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task GetProductAsync_NonexistentId_ReturnsNotFound()
        {
            // Arrange
            var nonexistentProductId = 1;

            // Act
            var response = await _context.HttpClient.GetAsync($"/api/products/{nonexistentProductId}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task UpdateProductAsync_ValidRequest_ReturnsOkResult()
        {
            // Arrange
            var addProductRequest = new AddProductRequest
            {
                Name = "Product1",
                Description = "Description1",
                SKU = "SKU1"
            };

            // Act
            var addProductResponse = await _context.HttpClient.PostAsJsonAsync("/api/products", addProductRequest);
            addProductResponse.EnsureSuccessStatusCode();

            var addedProduct = await addProductResponse.Content.ReadFromJsonAsync<ProductResponse>();

            addedProduct.Should().NotBeNull();

            var updateProductRequest = new UpdateProductRequest
            {
                Id = addedProduct!.Id,
                Name = "UpdatedProduct",
                Description = "UpdatedDescription",
                SKU = "UpdatedSKU"
            };

            // Act
            var updateProductResponse = await _context.HttpClient.PutAsJsonAsync("/api/products", updateProductRequest);
            updateProductResponse.EnsureSuccessStatusCode();

            var updatedProduct = await updateProductResponse.Content.ReadFromJsonAsync<ProductResponse>();

            // Assert
            updateProductResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            updatedProduct.Should().NotBeNull().And.BeEquivalentTo(updateProductRequest);
        }

        [Fact]
        public async Task UpdateProductAsync_InvalidRequest_ReturnsBadRequest()
        {
            // Arrange
            var updateProductRequest = new UpdateProductRequest();

            // Act
            var response = await _context.HttpClient.PutAsJsonAsync("/api/products", updateProductRequest);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task DeleteProductAsync_ExistingId_ReturnsOkResult()
        {
            // Arrange
            var addProductRequest = new AddProductRequest
            {
                Name = "Product1",
                Description = "Description1",
                SKU = "SKU1"
            };

            // Act
            var addProductResponse = await _context.HttpClient.PostAsJsonAsync("/api/products", addProductRequest);
            addProductResponse.EnsureSuccessStatusCode();

            var addedProduct = await addProductResponse.Content.ReadFromJsonAsync<ProductResponse>();

            addedProduct.Should().NotBeNull();

            var deleteProductResponse = await _context.HttpClient.DeleteAsync($"/api/products/{addedProduct!.Id}");
            deleteProductResponse.EnsureSuccessStatusCode();

            // Assert
            deleteProductResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task DeleteProductAsync_NonexistentId_ReturnsNotFound()
        {
            // Arrange
            var nonexistentProductId = 1;

            // Act
            var response = await _context.HttpClient.DeleteAsync($"/api/products/{nonexistentProductId}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}