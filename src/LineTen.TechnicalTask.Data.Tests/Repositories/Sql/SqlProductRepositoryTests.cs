using AutoMapper;
using LineTen.TechnicalTask.Data.DbContexts;
using LineTen.TechnicalTask.Data.Entities.Sql;
using LineTen.TechnicalTask.Data.Mappings;
using LineTen.TechnicalTask.Data.Tests.Fixtures;
using LineTen.TechnicalTask.Data.Tests.Helpers;
using LineTen.TechnicalTask.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace LineTen.TechnicalTask.Data.Tests.Repositories.Sql
{
    public interface IProductRepository
    {
        Task<Product> AddProductAsync(Product newProduct, CancellationToken cancellationToken = default);

        Task<bool> DeleteProductAsync(int id, CancellationToken cancellationToken = default);

        Task<ICollection<Product>> GetAllProductsAsync(CancellationToken cancellationToken = default);

        Task<Product?> GetProductAsync(int id, CancellationToken cancellationToken = default);

        Task<Product?> UpdateProductAsync(Product updatedProduct, CancellationToken cancellationToken = default);
    }

    public class SqlProductRepository : IProductRepository
    {
        private readonly TechnicalTestContext _context;
        private readonly IMapper _mapper;

        public SqlProductRepository(TechnicalTestContext context, IMapper mapper)
        {
            ArgumentNullException.ThrowIfNull(context);
            ArgumentNullException.ThrowIfNull(mapper);

            _context = context;
            _mapper = mapper;
        }

        public async Task<Product> AddProductAsync(Product newProduct, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(newProduct);

            var entity = _mapper.Map<Product, ProductEntity>(newProduct);

            await _context.Products.AddAsync(entity, cancellationToken).ConfigureAwait(false);

            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return _mapper.Map<ProductEntity, Product>(entity);
        }

        public async Task<bool> DeleteProductAsync(int id, CancellationToken cancellationToken = default)
        {
            ArgumentOutOfRangeException.ThrowIfZero(id);

            var entity = await _context.Products.FindAsync([id], cancellationToken).ConfigureAwait(false);

            if (entity is null)
            {
                return false;
            }

            _context.Products.Remove(entity);

            _ = await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return true;
        }

        public async Task<ICollection<Product>> GetAllProductsAsync(CancellationToken cancellationToken = default)
        {
            // Enhance: May want to consider using an IEnumerable depending on how many customers we intend to support
            var products = await _context.Products
                .Select(p => _mapper.Map<ProductEntity, Product>(p))
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);

            return products;
        }

        public async Task<Product?> GetProductAsync(int id, CancellationToken cancellationToken = default)
        {
            ArgumentOutOfRangeException.ThrowIfZero(id);

            var entity = await _context.Products.FindAsync([id], cancellationToken).ConfigureAwait(false);

            if (entity is null)
            {
                return null;
            }

            return _mapper.Map<ProductEntity, Product>(entity);
        }

        public async Task<Product?> UpdateProductAsync(Product updatedProduct, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(updatedProduct);
            ArgumentOutOfRangeException.ThrowIfZero(updatedProduct.Id);

            var entity = await _context.Products.FindAsync([updatedProduct.Id], cancellationToken).ConfigureAwait(false);

            if (entity is null)
            {
                return null;
            }

            _mapper.Map(updatedProduct, entity);

            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return _mapper.Map<ProductEntity, Product>(entity);
        }
    }

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