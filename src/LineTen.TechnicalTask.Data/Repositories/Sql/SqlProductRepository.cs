using AutoMapper;
using LineTen.TechnicalTask.Data.DbContexts;
using LineTen.TechnicalTask.Data.Entities.Sql;
using LineTen.TechnicalTask.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace LineTen.TechnicalTask.Data.Repositories.Sql
{
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
}