using LineTen.TechnicalTask.Data.Entities.Sql;
using LineTen.TechnicalTask.Domain.Enums;
using LineTen.TechnicalTask.Domain.Models;

namespace LineTen.TechnicalTask.Data.Tests.Helpers
{
    public static class TestHelper
    {
        public static Customer CreateCustomer(int? id)
        {
            return new Customer
            {
                Id = id ?? default,
                FirstName = $"Customer{id}FirstName",
                LastName = $"Customer{id}LastName",
                Phone = $"Customer{id}Phone",
                Email = $"Customer{id}Email"
            };
        }

        public static CustomerEntity CreateCustomerEntity(int? id)
        {
            return new CustomerEntity
            {
                Id = id ?? default,
                FirstName = $"Customer{id}FirstName",
                LastName = $"Customer{id}LastName",
                Phone = $"Customer{id}Phone",
                Email = $"Customer{id}Email"
            };
        }

        public static Product CreateProduct(int? id)
        {
            return new Product
            {
                Id = id ?? default,
                Name = $"Product{id}Name",
                Description = $"Product{id}Description",
                SKU = $"Product{id}SKU"
            };
        }

        public static ProductEntity CreateProductEntity(int? id)
        {
            return new ProductEntity
            {
                Id = id ?? default,
                Name = $"Product{id}Name",
                Description = $"Product{id}Description",
                SKU = $"Product{id}SKU"
            };
        }

        public static Order CreateOrder(int? id, int productId, int customerId)
        {
            return new Order
            {
                Id = id ?? default,
                ProductId = productId,
                CustomerId = customerId,
                Status = OrderStatus.New,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            };
        }

        public static OrderEntity CreateOrderEntity(int? id, int productId, int customerId)
        {
            return new OrderEntity
            {
                Id = id ?? default,
                ProductId = productId,
                CustomerId = customerId,
                Status = (int)OrderStatus.New,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            };
        }
    }
}