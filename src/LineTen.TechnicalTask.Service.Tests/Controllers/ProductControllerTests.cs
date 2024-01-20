using AutoMapper;
using LineTen.TechnicalTask.Domain.Models;
using LineTen.TechnicalTask.Service.Controllers;
using LineTen.TechnicalTask.Service.Domain.Mappings;
using LineTen.TechnicalTask.Service.Domain.Models;
using LineTen.TechnicalTask.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LineTen.TechnicalTask.Service.Tests.Controllers
{
    public class ProductControllerTests
    {
        private readonly ProductController _testClass;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductController> _logger;

        public ProductControllerTests()
        {
            _productService = Substitute.For<IProductService>();
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<RequestModelProfile>();
                cfg.AddProfile<ResponseModelProfile>();
            }).CreateMapper();

            _logger = Substitute.For<ILogger<ProductController>>();

            _testClass = new ProductController(_productService, _mapper, _logger);
        }

        [Fact]
        public void CanConstruct()
        {
            // Act
            var instance = new ProductController(_productService, _mapper, _logger);

            // Assert
            instance.Should().NotBeNull();
        }

        [Fact]
        public void CannotConstructWithNullProductService()
        {
            FluentActions.Invoking(() => new ProductController(default(IProductService), _mapper, _logger)).Should().Throw<ArgumentNullException>().WithParameterName("productService");
        }

        [Fact]
        public void CannotConstructWithNullMapper()
        {
            FluentActions.Invoking(() => new ProductController(_productService, default(IMapper), _logger)).Should().Throw<ArgumentNullException>().WithParameterName("mapper");
        }

        [Fact]
        public void CannotConstructWithNullLogger()
        {
            FluentActions.Invoking(() => new ProductController(_productService, _mapper, default(ILogger<ProductController>))).Should().Throw<ArgumentNullException>().WithParameterName("logger");
        }

        [Fact]
        public async Task CanCallAddProductAsync()
        {
            // Arrange
            var addProductRequest = new AddProductRequest
            {
                Name = "TestProduct",
                Description = "TestDescription",
                SKU = "TestSKU"
            };

            var newProduct = new Product
            {
                Id = 1,
                Name = "TestProduct",
                Description = "TestDescription",
                SKU = "TestSKU"
            };

            _productService.AddProductAsync(Arg.Any<Product>(), Arg.Any<CancellationToken>()).Returns(newProduct);

            // Act
            var actionResult = await _testClass.AddProductAsync(addProductRequest, CancellationToken.None) as OkObjectResult;
            var result = actionResult!.Value as ProductResponse;

            // Assert
            await _productService.Received(1).AddProductAsync(Arg.Any<Product>(), Arg.Any<CancellationToken>());

            result.Should().BeEquivalentTo(newProduct).And.BeEquivalentTo(addProductRequest);
        }

        [Fact]
        public async Task CanCallGetAllProductsAsync()
        {
            // Arrange
            var products = Substitute.For<ICollection<Product>>();

            _productService.GetAllProductsAsync(Arg.Any<CancellationToken>())
                .Returns(products);

            // Act
            var actionResult = await _testClass.GetAllProductsAsync(CancellationToken.None) as OkObjectResult;
            var result = actionResult!.Value as ICollection<ProductResponse>;

            // Assert
            await _productService.Received().GetAllProductsAsync(Arg.Any<CancellationToken>());

            result.Should().BeEquivalentTo(products);
        }

        [Fact]
        public async Task CanCallGetProductAsync()
        {
            // Arrange
            var id = 123;
            var product = new Product
            {
                Id = 123,
                Name = "TestProduct",
                Description = "TestDescription",
                SKU = "TestSKU"
            };

            _productService.GetProductAsync(id, Arg.Any<CancellationToken>()).Returns(product);

            // Act
            var actionResult = await _testClass.GetProductAsync(id, CancellationToken.None) as OkObjectResult;
            var result = actionResult!.Value as ProductResponse;

            // Assert
            await _productService.Received().GetProductAsync(id, Arg.Any<CancellationToken>());

            result.Should().BeEquivalentTo(product);
        }

        [Fact]
        public async Task CannotCallGetProductAsyncWithInvalidId()
        {
            // Arrange
            var invalidId = default(int);

            // Act
            var actionResult = await _testClass.GetProductAsync(invalidId, CancellationToken.None) as BadRequestObjectResult;

            // Assert
            actionResult.Should().NotBeNull();
            actionResult!.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        [Fact]
        public async Task CannotCallGetProductAsyncWithNonexistentId()
        {
            // Arrange
            var nonExistentId = 1;

            _productService.GetProductAsync(nonExistentId, Arg.Any<CancellationToken>()).Returns(default(Product?));

            // Act
            var actionResult = await _testClass.GetProductAsync(nonExistentId, CancellationToken.None) as NotFoundResult;

            // Assert
            actionResult.Should().NotBeNull();
            actionResult!.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }

        [Fact]
        public async Task CanCallUpdateProductAsync()
        {
            // Arrange
            var updateProductRequest = new UpdateProductRequest
            {
                Id = 567,
                Name = "UpdatedProduct",
                Description = "UpdatedDescription",
                SKU = "UpdatedSKU"
            };

            var product = new Product
            {
                Id = 567,
                Name = "UpdatedProduct",
                Description = "UpdatedDescription",
                SKU = "UpdatedSKU"
            };

            var cancellationToken = CancellationToken.None;

            _productService.UpdateProductAsync(Arg.Any<Product>(), Arg.Any<CancellationToken>()).Returns(product);

            // Act
            var actionResult = await _testClass.UpdateProductAsync(updateProductRequest, cancellationToken) as OkObjectResult;
            var result = actionResult!.Value as ProductResponse;

            // Assert
            await _productService.Received().UpdateProductAsync(Arg.Any<Product>(), Arg.Any<CancellationToken>());

            result.Should().BeEquivalentTo(product).And.BeEquivalentTo(updateProductRequest);
        }

        [Fact]
        public async Task CanCallDeleteProductAsync()
        {
            // Arrange
            var id = 456;
            var cancellationToken = CancellationToken.None;

            _productService.DeleteProductAsync(id, Arg.Any<CancellationToken>()).Returns(true);

            // Act
            var actionResult = await _testClass.DeleteProductAsync(id, cancellationToken);

            // Assert
            await _productService.Received(1).DeleteProductAsync(id, Arg.Any<CancellationToken>());

            actionResult.Should().BeOfType<OkResult>();
        }

        [Fact]
        public async Task CannotCallDeleteProductAsyncWithNonexistentId()
        {
            // Arrange
            var nonExistentId = 1;

            _productService.DeleteProductAsync(nonExistentId, Arg.Any<CancellationToken>()).Returns(false);

            // Act
            var actionResult = await _testClass.DeleteProductAsync(nonExistentId, CancellationToken.None) as NotFoundResult;

            // Assert
            actionResult.Should().NotBeNull();
            actionResult!.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }
    }
}