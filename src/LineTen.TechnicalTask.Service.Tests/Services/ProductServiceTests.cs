using LineTen.TechnicalTask.Data.Repositories;
using LineTen.TechnicalTask.Domain.Models;

namespace LineTen.TechnicalTask.Service.Tests.Services
{
    public interface IProductService
    {
        Task<Product> AddProductAsync(Product newProduct, CancellationToken cancellationToken = default);

        Task<bool> DeleteProductAsync(int id, CancellationToken cancellationToken = default);

        Task<ICollection<Product>> GetAllProductsAsync(CancellationToken cancellationToken = default);

        Task<Product?> GetProductAsync(int id, CancellationToken cancellationToken = default);

        Task<Product?> UpdateProductAsync(Product updatedProduct, CancellationToken cancellationToken = default);
    }

    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            ArgumentNullException.ThrowIfNull(productRepository);

            _productRepository = productRepository;
        }

        public Task<Product> AddProductAsync(Product newProduct, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(newProduct, nameof(newProduct));

            return _productRepository.AddProductAsync(newProduct, cancellationToken);
        }

        public Task<bool> DeleteProductAsync(int id, CancellationToken cancellationToken = default)
        {
            ArgumentOutOfRangeException.ThrowIfZero(id);

            return _productRepository.DeleteProductAsync(id, cancellationToken);
        }

        public Task<ICollection<Product>> GetAllProductsAsync(CancellationToken cancellationToken = default)
        {
            return _productRepository.GetAllProductsAsync(cancellationToken);
        }

        public Task<Product?> GetProductAsync(int id, CancellationToken cancellationToken = default)
        {
            ArgumentOutOfRangeException.ThrowIfZero(id);

            return _productRepository.GetProductAsync(id, cancellationToken);
        }

        public Task<Product?> UpdateProductAsync(Product updatedProduct, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(updatedProduct, nameof(updatedProduct));
            ArgumentOutOfRangeException.ThrowIfZero(updatedProduct.Id);

            return _productRepository.UpdateProductAsync(updatedProduct, cancellationToken);
        }
    }

    public class ProductServiceTests
    {
        private readonly ProductService _testClass;
        private readonly IProductRepository _productRepository;

        public ProductServiceTests()
        {
            _productRepository = Substitute.For<IProductRepository>();
            _testClass = new ProductService(_productRepository);
        }

        [Fact]
        public void CanConstruct()
        {
            // Act
            var instance = new ProductService(_productRepository);

            // Assert
            instance.Should().NotBeNull();
        }

        [Fact]
        public void CannotConstructWithNullProductRepository()
        {
            FluentActions.Invoking(() => new ProductService(default(IProductRepository))).Should().Throw<ArgumentNullException>().WithParameterName("productRepository");
        }

        [Fact]
        public async Task CanCallAddProductAsync()
        {
            // Arrange
            var product = new Product
            {
                Id = 1167181025,
                Name = "TestValue267366161",
                Description = "TestValue708177916",
                SKU = "TestValue1601148797"
            };

            _productRepository.AddProductAsync(Arg.Any<Product>(), Arg.Any<CancellationToken>()).Returns(product);

            // Act
            var result = await _testClass.AddProductAsync(product, CancellationToken.None);

            // Assert
            await _productRepository.Received(1).AddProductAsync(Arg.Any<Product>(), Arg.Any<CancellationToken>());

            result.Should().BeEquivalentTo(product);
        }

        [Fact]
        public async Task CannotCallAddProductAsyncWithNullNewProduct()
        {
            await FluentActions.Invoking(() => _testClass.AddProductAsync(default(Product), CancellationToken.None)).Should().ThrowAsync<ArgumentNullException>().WithParameterName("newProduct");
        }

        [Fact]
        public async Task CanCallDeleteProductAsync()
        {
            // Arrange
            var id = 1827235578;

            _productRepository.DeleteProductAsync(Arg.Any<int>(), Arg.Any<CancellationToken>()).Returns(true);

            // Act
            var result = await _testClass.DeleteProductAsync(id, CancellationToken.None);

            // Assert
            await _productRepository.Received(1).DeleteProductAsync(id, Arg.Any<CancellationToken>());

            result.Should().BeTrue();
        }

        [Fact]
        public async Task CanCallGetAllProductsAsync()
        {
            // Arrange
            var products = Substitute.For<ICollection<Product>>();

            _productRepository.GetAllProductsAsync(Arg.Any<CancellationToken>()).Returns(products);

            // Act
            var result = await _testClass.GetAllProductsAsync(CancellationToken.None);

            // Assert
            await _productRepository.Received(1).GetAllProductsAsync(Arg.Any<CancellationToken>());

            result.Should().BeEquivalentTo(products);
        }

        [Fact]
        public async Task CanCallGetProductAsync()
        {
            // Arrange
            var id = 1599543432;

            var product = new Product
            {
                Id = 1599543432,
                Name = "TestValue20175351",
                Description = "TestValue1139568566",
                SKU = "TestValue753943285"
            };

            _productRepository.GetProductAsync(Arg.Any<int>(), Arg.Any<CancellationToken>()).Returns(product);

            // Act
            var result = await _testClass.GetProductAsync(id, CancellationToken.None);

            // Assert
            await _productRepository.Received(1).GetProductAsync(id, Arg.Any<CancellationToken>());

            result.Should().BeEquivalentTo(product);
        }

        [Fact]
        public async Task CanCallUpdateProductAsync()
        {
            // Arrange
            var updatedProduct = new Product
            {
                Id = 1794942182,
                Name = "TestValue611549683",
                Description = "TestValue300853694",
                SKU = "TestValue1832963413"
            };

            _productRepository.UpdateProductAsync(Arg.Any<Product>(), Arg.Any<CancellationToken>()).Returns(updatedProduct);

            // Act
            var result = await _testClass.UpdateProductAsync(updatedProduct, CancellationToken.None);

            // Assert
            await _productRepository.Received(1).UpdateProductAsync(Arg.Any<Product>(), Arg.Any<CancellationToken>());

            result.Should().BeEquivalentTo(updatedProduct);
        }

        [Fact]
        public async Task CannotCallUpdateProductAsyncWithNullUpdatedProduct()
        {
            await FluentActions.Invoking(() => _testClass.UpdateProductAsync(default(Product), CancellationToken.None)).Should().ThrowAsync<ArgumentNullException>().WithParameterName("updatedProduct");
        }

        [Fact]
        public async Task CannotCallUpdateProductAsyncWithDefaultId()
        {
            // Arrange
            var invalidProduct = new Product
            {
                Id = default,
                Name = "InvalidProductName",
                Description = "InvalidProductDescription",
                SKU = "InvalidProductSKU"
            };

            // Assert
            await FluentActions.Invoking(() => _testClass.UpdateProductAsync(invalidProduct, CancellationToken.None)).Should().ThrowAsync<ArgumentOutOfRangeException>().WithParameterName("updatedProduct.Id");
        }
    }
}