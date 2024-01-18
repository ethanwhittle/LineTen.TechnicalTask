using AutoMapper;
using LineTen.TechnicalTask.Domain.Enums;
using LineTen.TechnicalTask.Domain.Models;
using LineTen.TechnicalTask.Service.Domain.Mappings;
using LineTen.TechnicalTask.Service.Domain.Models;

namespace LineTen.TechnicalTask.Service.Domain.Tests.Mappings
{
    public class RequestModelProfileTests
    {
        private readonly MapperConfiguration _mapperConfiguration;
        private readonly IMapper _mapper;

        public RequestModelProfileTests()
        {
            _mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile<RequestModelProfile>());
            _mapper = _mapperConfiguration.CreateMapper();
        }

        [Fact]
        public void CanConstruct()
        {
            // Act
            var instance = new RequestModelProfile();

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
        public void MapsAddCustomerRequestToCustomer()
        {
            // Arrange
            var request = new AddCustomerRequest
            {
                FirstName = "John",
                LastName = "Doe",
                Phone = "+1-555-1234",
                Email = "john.doe@example.com"
            };

            // Act
            var mapped = _mapper.Map<AddCustomerRequest, Customer>(request);

            // Assert
            mapped.Should().NotBeNull().And.BeOfType<Customer>();
            mapped.FirstName.Should().Be(request.FirstName);
            mapped.LastName.Should().Be(request.LastName);
            mapped.Phone.Should().Be(request.Phone);
            mapped.Email.Should().Be(request.Email);
        }

        [Fact]
        public void MapsUpdateCustomerRequestToCustomer()
        {
            // Arrange
            var request = new UpdateCustomerRequest
            {
                Id = 1,
                FirstName = "Jane",
                LastName = "Doe",
                Phone = "+1-555-5678",
                Email = "jane.doe@example.com"
            };

            // Act
            var mapped = _mapper.Map<UpdateCustomerRequest, Customer>(request);

            // Assert
            mapped.Should().NotBeNull().And.BeOfType<Customer>();
            mapped.Id.Should().Be(request.Id);
            mapped.FirstName.Should().Be(request.FirstName);
            mapped.LastName.Should().Be(request.LastName);
            mapped.Phone.Should().Be(request.Phone);
            mapped.Email.Should().Be(request.Email);
        }

        [Fact]
        public void MapsAddProductRequestToProduct()
        {
            // Arrange
            var request = new AddProductRequest
            {
                Name = "Product Name",
                Description = "Product Description",
                SKU = "ABC123"
            };

            // Act
            var mapped = _mapper.Map<AddProductRequest, Product>(request);

            // Assert
            mapped.Should().NotBeNull().And.BeOfType<Product>();
            mapped.Name.Should().Be(request.Name);
            mapped.Description.Should().Be(request.Description);
            mapped.SKU.Should().Be(request.SKU);
        }

        [Fact]
        public void MapsUpdateProductRequestToProduct()
        {
            // Arrange
            var request = new UpdateProductRequest
            {
                Id = 1,
                Name = "Updated Product Name",
                Description = "Updated Product Description",
                SKU = "XYZ789"
            };

            // Act
            var mapped = _mapper.Map<UpdateProductRequest, Product>(request);

            // Assert
            mapped.Should().NotBeNull().And.BeOfType<Product>();
            mapped.Id.Should().Be(request.Id);
            mapped.Name.Should().Be(request.Name);
            mapped.Description.Should().Be(request.Description);
            mapped.SKU.Should().Be(request.SKU);
        }

        [Fact]
        public void MapsAddOrderRequestToOrder()
        {
            // Arrange
            var request = new AddOrderRequest
            {
                ProductId = 1,
                CustomerId = 2,
                Status = OrderStatus.Completed
            };

            // Act
            var mapped = _mapper.Map<AddOrderRequest, Order>(request);

            // Assert
            mapped.Should().NotBeNull().And.BeOfType<Order>();
            mapped.ProductId.Should().Be(request.ProductId);
            mapped.CustomerId.Should().Be(request.CustomerId);
            mapped.Status.Should().Be(request.Status);
        }

        [Fact]
        public void MapsUpdateOrderRequestToOrder()
        {
            // Arrange
            var request = new UpdateOrderRequest
            {
                Id = 1,
                ProductId = 3,
                CustomerId = 4,
                Status = OrderStatus.InProgress
            };

            // Act
            var mapped = _mapper.Map<UpdateOrderRequest, Order>(request);

            // Assert
            mapped.Should().NotBeNull().And.BeOfType<Order>();
            mapped.Id.Should().Be(request.Id);
            mapped.ProductId.Should().Be(request.ProductId);
            mapped.CustomerId.Should().Be(request.CustomerId);
            mapped.Status.Should().Be(request.Status);
        }
    }
}