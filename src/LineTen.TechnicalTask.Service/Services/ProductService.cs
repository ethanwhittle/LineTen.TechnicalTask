using LineTen.TechnicalTask.Data.Repositories;
using LineTen.TechnicalTask.Domain.Models;

namespace LineTen.TechnicalTask.Service.Services
{
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
}