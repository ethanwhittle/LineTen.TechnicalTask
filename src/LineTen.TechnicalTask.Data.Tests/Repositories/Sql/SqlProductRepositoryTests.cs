using AutoMapper;
using LineTen.TechnicalTask.Data.DbContexts;
using LineTen.TechnicalTask.Data.Mappings;
using LineTen.TechnicalTask.Data.Repositories.Sql;
using LineTen.TechnicalTask.Data.Tests.Fixtures;
using LineTen.TechnicalTask.Data.Tests.Helpers;
using LineTen.TechnicalTask.Domain.Models;

namespace LineTen.TechnicalTask.Data.Tests.Repositories.Sql
{
    public class SqlProductRepositoryTests : IClassFixture<TechnicalTestContextFixture>
    {
        private readonly SqlProductRepository _testClass;
        private readonly TechnicalTestContextFixture _fixture;
        private readonly TechnicalTestContext _context;
        private readonly IMapper _mapper;

        public SqlProductRepositoryTests(TechnicalTestContextFixture fixture)
        {
            _fixture = fixture;
            _context = fixture.CreateDbContext();
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<EntityModelProfile>()).CreateMapper();
            _testClass = new SqlProductRepository(_context, _mapper);
        }

        [Fact]
        public void CanConstruct()
        {
            // Act
            var instance = new SqlProductRepository(_context, _mapper);

            // Assert
            instance.Should().NotBeNull();
        }

        [Fact]
        public void CannotConstructWithNullContext()
        {
            FluentActions.Invoking(() => new SqlProductRepository(default(TechnicalTestContext), _mapper)).Should().Throw<ArgumentNullException>().WithParameterName("context");
        }

        [Fact]
        public void CannotConstructWithNullMapper()
        {
            FluentActions.Invoking(() => new SqlProductRepository(_context, default(IMapper))).Should().Throw<ArgumentNullException>().WithParameterName("mapper");
        }

        [Fact]
        public async Task CanCallAddProductAsync()
        {
            // Arrange
            var product = TestHelper.CreateProduct(1);

            using (var dbContext = _fixture.CreateDbContext())
            {
                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();
            }

            // Act
            var result = await _testClass.AddProductAsync(product, CancellationToken.None);

            // Assert
            result.Should().BeEquivalentTo(product);

            using (var dbContext = _fixture.CreateDbContext())
            {
                var savedProduct = dbContext.Products.Single();
                savedProduct.Id.Should().Be(product.Id);
                savedProduct.Name.Should().Be(product.Name);
                savedProduct.Description.Should().Be(product.Description);
                savedProduct.SKU.Should().Be(product.SKU);
            }
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
            var product = TestHelper.CreateProductEntity(1);

            using (var dbContext = _fixture.CreateDbContext())
            {
                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();
                dbContext.Products.Add(product);
                dbContext.SaveChanges();
            }

            // Act
            var result = await _testClass.DeleteProductAsync(1, CancellationToken.None);

            // Assert
            result.Should().BeTrue();

            using (var dbContext = _fixture.CreateDbContext())
            {
                var savedProduct = dbContext.Products.Should().BeEmpty();
            }
        }

        [Fact]
        public async Task CanCallGetAllProductsAsync()
        {
            // Arrange
            var products = Enumerable.Range(1, 10)
                .Select(p => TestHelper.CreateProductEntity(p))
                .ToList();

            using (var dbContext = _fixture.CreateDbContext())
            {
                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();
                dbContext.Products.AddRange(products);
                dbContext.SaveChanges();
            }

            // Act
            var result = await _testClass.GetAllProductsAsync(CancellationToken.None);

            // Assert
            result.Should().NotBeEmpty().And.HaveCount(10);
        }

        [Fact]
        public async Task CanCallGetProductAsync()
        {
            // Arrange
            var product = TestHelper.CreateProductEntity(1);

            using (var dbContext = _fixture.CreateDbContext())
            {
                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();
                dbContext.Products.Add(product);
                dbContext.SaveChanges();
            }

            // Act
            var result = await _testClass.GetProductAsync(1, CancellationToken.None);

            // Assert
            result.Should().NotBeNull().And.BeOfType<Product>();
            result!.Id.Should().Be(product.Id);
            result!.Name.Should().Be(product.Name);
            result!.Description.Should().Be(product.Description);
            result!.SKU.Should().Be(product.SKU);
        }

        [Fact]
        public async Task CanCallUpdateProductAsync()
        {
            // Arrange
            var product = TestHelper.CreateProductEntity(1);

            using (var dbContext = _fixture.CreateDbContext())
            {
                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();
                dbContext.Products.Add(product);
                dbContext.SaveChanges();
            }

            var updatedProduct = new Product
            {
                Id = 1,
                Name = $"NewProduct1Name",
                Description = $"NewProduct1Description",
                SKU = $"NewProduct1SKU"
            };

            // Act
            var result = await _testClass.UpdateProductAsync(updatedProduct, CancellationToken.None);

            // Assert
            result.Should().BeEquivalentTo(updatedProduct);

            using (var dbContext = _fixture.CreateDbContext())
            {
                var savedProduct = dbContext.Products.Single();
                savedProduct.Id.Should().Be(updatedProduct.Id);
                savedProduct.Name.Should().Be(updatedProduct.Name);
                savedProduct.Description.Should().Be(updatedProduct.Description);
                savedProduct.SKU.Should().Be(updatedProduct.SKU);
            }
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

            using (var dbContext = _fixture.CreateDbContext())
            {
                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();
            }

            // Act
            await FluentActions.Invoking(() => _testClass.UpdateProductAsync(invalidProduct, CancellationToken.None)).Should().ThrowAsync<ArgumentOutOfRangeException>().WithParameterName("updatedProduct.Id");

            // Assert
            using (var dbContext = _fixture.CreateDbContext())
            {
                dbContext.Customers.Should().BeEmpty();
            }
        }
    }
}