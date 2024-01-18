using AutoMapper;
using LineTen.TechnicalTask.Data.Tests.Entities.Sql;
using LineTen.TechnicalTask.Domain.Enums;
using LineTen.TechnicalTask.Domain.Models;

namespace LineTen.TechnicalTask.Data.Tests.Mappings
{
    public class EntityModelProfileTests
    {
        public class EntityModelProfile : Profile
        {
            public EntityModelProfile()
            {
                CreateMap<CustomerEntity, Customer>().ReverseMap();
                CreateMap<ProductEntity, Product>().ReverseMap();
                CreateMap<OrderEntity, Order>().ReverseMap();
            }
        }

        private readonly MapperConfiguration _mapperConfiguration;
        private readonly IMapper _mapper;

        public EntityModelProfileTests()
        {
            _mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile<EntityModelProfile>());
            _mapper = _mapperConfiguration.CreateMapper();
        }

        [Fact]
        public void CanConstruct()
        {
            // Act
            var instance = new EntityModelProfile();

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
        public void ReverseMapsCustomerEntityAndCustomer()
        {
            // Arrange
            var entity = new CustomerEntity
            {
                Id = 1,
                FirstName = "Bubbles",
                LastName = "Fluffington",
                Phone = "+1-555-1234",
                Email = "bubbles.fluffington@example.com",
            };
            
            // Act
            var mapped = _mapper.Map<CustomerEntity, Customer>(entity);

            // Assert
            mapped.Should().NotBeNull().And.BeOfType<Customer>();
            mapped.Id.Should().Be(entity.Id);
            mapped.FirstName.Should().Be(entity.FirstName);
            mapped.LastName.Should().Be(entity.LastName);
            mapped.Phone.Should().Be(entity.Phone);
            mapped.Email.Should().Be(entity.Email);

            // Act
            var mappedEntity = _mapper.Map<Customer, CustomerEntity>(mapped);

            // Assert
            mappedEntity.Should().BeEquivalentTo(entity, options => options.IncludingProperties());
        }

        [Fact]
        public void ReverseMapsProductEntityAndProduct()
        {
            // Arrange
            var entity = new ProductEntity
            {
                Id = 1,
                Name = "Tickle-Me-Feathers Pillow",
                Description = "The pillow that tickles back! Perfect for spontaneous giggles and unexpected nap-time adventures.",
                SKU = "TMF789"
            };

            // Act
            var mapped = _mapper.Map<ProductEntity, Product>(entity);

            // Assert
            mapped.Should().NotBeNull().And.BeOfType<Product>();
            mapped.Id.Should().Be(entity.Id);
            mapped.Name.Should().Be(entity.Name);
            mapped.Description.Should().Be(entity.Description);
            mapped.SKU.Should().Be(entity.SKU);

            // Act
            var mappedEntity = _mapper.Map<Product, ProductEntity>(mapped);

            // Assert
            mappedEntity.Should().BeEquivalentTo(entity, options => options.IncludingProperties());
        }

        [Fact]
        public void ReverseMapsOrderEntityAndOrder()
        {
            // Arrange
            var entity = new OrderEntity
            {
                ProductId = 1,
                CustomerId = 1,
                Status = (int)OrderStatus.InProgress,
                CreatedDate = DateTime.Now.AddDays(-1),
                UpdatedDate = DateTime.Now,
            };

            // Act
            var mapped = _mapper.Map<OrderEntity, Order>(entity);

            // Assert
            mapped.Should().NotBeNull().And.BeOfType<Order>();
            mapped.ProductId.Should().Be(entity.ProductId);
            mapped.CustomerId.Should().Be(entity.CustomerId);
            mapped.Status.Should().Be((OrderStatus)entity.Status);
            mapped.CreatedDate.Should().Be(entity.CreatedDate);
            mapped.UpdatedDate.Should().Be(entity.UpdatedDate);

            // Act
            var mappedEntity = _mapper.Map<Order, OrderEntity>(mapped);

            // Assert
            mappedEntity.Should().BeEquivalentTo(entity, options => options.IncludingProperties());
        }
    }
}