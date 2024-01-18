using LineTen.TechnicalTask.Domain.Models;

namespace LineTen.TechnicalTask.Data.Repositories
{
    public interface IProductRepository
    {
        Task<Product> AddProductAsync(Product newProduct, CancellationToken cancellationToken = default);

        Task<bool> DeleteProductAsync(int id, CancellationToken cancellationToken = default);

        Task<ICollection<Product>> GetAllProductsAsync(CancellationToken cancellationToken = default);

        Task<Product?> GetProductAsync(int id, CancellationToken cancellationToken = default);

        Task<Product?> UpdateProductAsync(Product updatedProduct, CancellationToken cancellationToken = default);
    }
}