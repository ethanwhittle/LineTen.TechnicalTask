using AutoMapper;
using LineTen.TechnicalTask.Domain.Enums;
using LineTen.TechnicalTask.Domain.Models;
using LineTen.TechnicalTask.Service.Domain.Mappings;
using LineTen.TechnicalTask.Service.Domain.Models;

namespace LineTen.TechnicalTask.Service.Domain.Tests.Mappings
{
    public class ResponseModelProfileTests
    {
        private readonly MapperConfiguration _mapperConfiguration;
        private readonly IMapper _mapper;

        public ResponseModelProfileTests()
        {
            _mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile<ResponseModelProfile>());
            _mapper = _mapperConfiguration.CreateMapper();
        }

        [Fact]
        public void CanConstruct()
        {
            // Act
            var instance = new ResponseModelProfile();

            // Assert
            instance.Should().NotBeNull();
        }

        [Fact]
        public void ConfigurationIsValid()
        {
            // Assert
            _mapperConfiguration.AssertConfigurationIsValid();
        }

        [Fact]
        public void MapsCustomerToCustomerResponse()
        {
            // Arrange
            var customer = new Customer
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Phone = "+1-555-1234",
                Email = "john.doe@example.com"
            };

            // Act
            var mapped = _mapper.Map<Customer, CustomerResponse>(customer);

            // Assert
            mapped.Should().NotBeNull().And.BeOfType<CustomerResponse>();
            mapped.Id.Should().Be(customer.Id);
            mapped.FirstName.Should().Be(customer.FirstName);
            mapped.LastName.Should().Be(customer.LastName);
            mapped.Phone.Should().Be(customer.Phone);
            mapped.Email.Should().Be(customer.Email);
        }

        [Fact]
        public void MapsProductToProductResponse()
        {
            // Arrange
            var product = new Product
            {
                Id = 1,
                Name = "Product Name",
                Description = "Product Description",
                SKU = "ABC123"
            };

            // Act
            var mapped = _mapper.Map<Product, ProductResponse>(product);

            // Assert
            mapped.Should().NotBeNull().And.BeOfType<ProductResponse>();
            mapped.Id.Should().Be(product.Id);
            mapped.Name.Should().Be(product.Name);
            mapped.Description.Should().Be(product.Description);
            mapped.SKU.Should().Be(product.SKU);
        }

        [Fact]
        public void MapsOrderToOrderResponse()
        {
            // Arrange
            var order = new Order
            {
                Id = 1,
                ProductId = 2,
                CustomerId = 3,
                Status = OrderStatus.Completed,
                CreatedDate = DateTime.Now.AddDays(-2),
                UpdatedDate = DateTime.Now.AddDays(-1)
            };

            // Act
            var mapped = _mapper.Map<Order, OrderResponse>(order);

            // Assert
            mapped.Should().NotBeNull().And.BeOfType<OrderResponse>();
            mapped.Id.Should().Be(order.Id);
            mapped.ProductId.Should().Be(order.ProductId);
            mapped.CustomerId.Should().Be(order.CustomerId);
            mapped.Status.Should().Be(order.Status);
            mapped.CreatedDate.Should().Be(order.CreatedDate);
            mapped.UpdatedDate.Should().Be(order.UpdatedDate);
        }
    }
}