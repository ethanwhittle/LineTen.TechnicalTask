﻿using AutoMapper;
using LineTen.TechnicalTask.Data.DbContexts;
using LineTen.TechnicalTask.Data.Entities.Sql;
using LineTen.TechnicalTask.Data.Mappings;
using LineTen.TechnicalTask.Data.Tests.Fixtures;
using LineTen.TechnicalTask.Data.Tests.Helpers;
using LineTen.TechnicalTask.Domain.Enums;
using LineTen.TechnicalTask.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace LineTen.TechnicalTask.Data.Tests.Repositories.Sql
{
    public interface IOrderRepository
    {
        Task<Order> AddOrderAsync(Order newOrder, CancellationToken cancellationToken = default);

        Task<bool> DeleteOrderAsync(int id, CancellationToken cancellationToken = default);

        // ENHANCE: Use case for GetAllOrdersAsync(int customerId) or GetAllOrdersAsync(int productId)?
        Task<ICollection<Order>> GetAllOrdersAsync(CancellationToken cancellationToken = default);

        Task<Order?> GetOrderAsync(int id, CancellationToken cancellationToken = default);

        Task<Order?> UpdateOrderAsync(Order updatedOrder, CancellationToken cancellationToken = default);
    }

    public class SqlOrderRepository : IOrderRepository
    {
        private readonly TechnicalTestContext _context;
        private readonly IMapper _mapper;

        public SqlOrderRepository(TechnicalTestContext context, IMapper mapper)
        {
            ArgumentNullException.ThrowIfNull(context);
            ArgumentNullException.ThrowIfNull(mapper);

            _context = context;
            _mapper = mapper;
        }

        public async Task<Order> AddOrderAsync(Order newOrder, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(newOrder);

            var entity = _mapper.Map<Order, OrderEntity>(newOrder);

            await _context.Orders.AddAsync(entity, cancellationToken).ConfigureAwait(false);

            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return _mapper.Map<OrderEntity, Order>(entity);
        }

        public async Task<bool> DeleteOrderAsync(int id, CancellationToken cancellationToken = default)
        {
            ArgumentOutOfRangeException.ThrowIfZero(id);

            var entity = await _context.Orders.FindAsync([id], cancellationToken).ConfigureAwait(false);

            if (entity is null)
            {
                return false;
            }

            _context.Orders.Remove(entity);

            _ = await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return true;
        }

        public async Task<ICollection<Order>> GetAllOrdersAsync(CancellationToken cancellationToken = default)
        {
            var orders = await _context.Orders
                .Select(o => _mapper.Map<OrderEntity, Order>(o))
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);

            return orders;
        }

        public async Task<Order?> GetOrderAsync(int id, CancellationToken cancellationToken = default)
        {
            ArgumentOutOfRangeException.ThrowIfZero(id);

            var entity = await _context.Orders.FindAsync([id], cancellationToken).ConfigureAwait(false);

            if (entity is null)
            {
                return null;
            }

            return _mapper.Map<OrderEntity, Order>(entity);
        }

        public async Task<Order?> UpdateOrderAsync(Order updatedOrder, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(updatedOrder);
            ArgumentOutOfRangeException.ThrowIfZero(updatedOrder.Id);

            var entity = await _context.Orders.FindAsync([updatedOrder.Id], cancellationToken).ConfigureAwait(false);

            if (entity is null)
            {
                return null;
            }

            _mapper.Map(updatedOrder, entity);

            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return _mapper.Map<OrderEntity, Order>(entity);
        }
    }

    public class SqlOrderRepositoryTests : IClassFixture<TechnicalTestContextFixture>
    {
        private readonly SqlOrderRepository _testClass;
        private readonly TechnicalTestContextFixture _fixture;
        private readonly TechnicalTestContext _context;
        private readonly IMapper _mapper;

        public SqlOrderRepositoryTests(TechnicalTestContextFixture fixture)
        {
            _fixture = fixture;
            _context = fixture.CreateDbContext();
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<EntityModelProfile>()).CreateMapper();
            _testClass = new SqlOrderRepository(_context, _mapper);
        }

        [Fact]
        public void CanConstruct()
        {
            // Act
            var instance = new SqlOrderRepository(_context, _mapper);

            // Assert
            instance.Should().NotBeNull();
        }

        [Fact]
        public void CannotConstructWithNullContext()
        {
            FluentActions.Invoking(() => new SqlOrderRepository(default(TechnicalTestContext), _mapper)).Should().Throw<ArgumentNullException>().WithParameterName("context");
        }

        [Fact]
        public void CannotConstructWithNullMapper()
        {
            FluentActions.Invoking(() => new SqlOrderRepository(_context, default(IMapper))).Should().Throw<ArgumentNullException>().WithParameterName("mapper");
        }

        [Fact]
        public async Task CanCallAddOrderAsync()
        {
            // Arrange
            var customer = TestHelper.CreateCustomerEntity(1);
            var product = TestHelper.CreateProductEntity(1);
            var order = TestHelper.CreateOrder(1, 1, 1);

            using (var dbContext = _fixture.CreateDbContext())
            {
                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();
                dbContext.Customers.Add(customer);
                dbContext.Products.Add(product);
                dbContext.SaveChanges();
            }

            // Act
            var result = await _testClass.AddOrderAsync(order, CancellationToken.None);

            // Assert
            result.Should().BeEquivalentTo(order);

            using (var dbContext = _fixture.CreateDbContext())
            {
                var savedOrder = dbContext.Orders.Single();
                savedOrder.Id.Should().Be(order.Id);
                savedOrder.ProductId.Should().Be(order.ProductId);
                savedOrder.CustomerId.Should().Be(order.CustomerId);
                savedOrder.Status.Should().Be((int)OrderStatus.New);
            }
        }

        [Fact]
        public async Task CannotCallAddOrderAsyncWithNullNewOrder()
        {
            await FluentActions.Invoking(() => _testClass.AddOrderAsync(default(Order), CancellationToken.None)).Should().ThrowAsync<ArgumentNullException>().WithParameterName("newOrder");
        }

        [Fact]
        public async Task CanCallDeleteOrderAsync()
        {
            // Arrange
            var customer = TestHelper.CreateCustomerEntity(1);
            var product = TestHelper.CreateProductEntity(1);
            var order = TestHelper.CreateOrderEntity(1, 1, 1);

            using (var dbContext = _fixture.CreateDbContext())
            {
                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();
                dbContext.Customers.Add(customer);
                dbContext.Products.Add(product);
                dbContext.Orders.Add(order);
                dbContext.SaveChanges();
            }

            // Act
            var result = await _testClass.DeleteOrderAsync(1, CancellationToken.None);

            // Assert
            result.Should().BeTrue();

            using (var dbContext = _fixture.CreateDbContext())
            {
                var savedOrder = dbContext.Orders.Should().BeEmpty();
            }
        }

        [Fact]
        public async Task CanCallGetAllOrdersAsync()
        {
            // Arrange
            var customer = TestHelper.CreateCustomerEntity(1);
            var product = TestHelper.CreateProductEntity(1);
            var orders = Enumerable.Range(1, 10)
                .Select(o => TestHelper.CreateOrderEntity(o, 1, 1))
                .ToList();

            using (var dbContext = _fixture.CreateDbContext())
            {
                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();
                dbContext.Customers.Add(customer);
                dbContext.Products.Add(product);
                dbContext.Orders.AddRange(orders);
                dbContext.SaveChanges();
            }

            // Act
            var result = await _testClass.GetAllOrdersAsync(CancellationToken.None);

            // Assert
            result.Should().NotBeEmpty().And.HaveCount(10);
        }

        [Fact]
        public async Task CanCallGetOrderAsync()
        {
            // Arrange
            var customer = TestHelper.CreateCustomerEntity(1);
            var product = TestHelper.CreateProductEntity(1);
            var order = TestHelper.CreateOrderEntity(1, 1, 1);

            using (var dbContext = _fixture.CreateDbContext())
            {
                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();
                dbContext.Customers.Add(customer);
                dbContext.Products.Add(product);
                dbContext.Orders.Add(order);
                dbContext.SaveChanges();
            }

            // Act
            var result = await _testClass.GetOrderAsync(1, CancellationToken.None);

            // Assert
            result.Should().NotBeNull().And.BeOfType<Order>();
            result!.Id.Should().Be(order.Id);
            result!.ProductId.Should().Be(order.ProductId);
            result!.CustomerId.Should().Be(order.CustomerId);            
            result!.Status.Should().Be((OrderStatus)order.Status);
        }

        [Fact]
        public async Task CanCallUpdateOrderAsync()
        {
            // Arrange
            var customer = TestHelper.CreateCustomerEntity(1);
            var product = TestHelper.CreateProductEntity(1);
            var order = TestHelper.CreateOrderEntity(1, 1, 1);

            using (var dbContext = _fixture.CreateDbContext())
            {
                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();
                dbContext.Customers.Add(customer);
                dbContext.Products.Add(product);
                dbContext.Orders.Add(order);
                dbContext.SaveChanges();
            }

            var updatedOrder = new Order
            {
                Id = 1,
                CustomerId = 1,
                ProductId = 1,
                Status = OrderStatus.Completed
            };

            // Act
            var result = await _testClass.UpdateOrderAsync(updatedOrder, CancellationToken.None);

            // Assert
            result.Should().BeEquivalentTo(updatedOrder);

            using (var dbContext = _fixture.CreateDbContext())
            {
                var savedOrder = dbContext.Orders.Single();
                savedOrder.Id.Should().Be(updatedOrder.Id);
                savedOrder.ProductId.Should().Be(updatedOrder.ProductId);
                savedOrder.CustomerId.Should().Be(updatedOrder.CustomerId);                
                savedOrder.Status.Should().Be((int)updatedOrder.Status);
                savedOrder.UpdatedDate.Should().NotBe(order.UpdatedDate);
            }
        }

        [Fact]
        public async Task CannotCallUpdateOrderAsyncWithNullUpdatedOrder()
        {
            await FluentActions.Invoking(() => _testClass.UpdateOrderAsync(default(Order), CancellationToken.None)).Should().ThrowAsync<ArgumentNullException>().WithParameterName("updatedOrder");
        }

        [Fact]
        public async Task CannotCallUpdateOrderAsyncWithDefaultId()
        {
            // Arrange
            var invalidOrder = new Order
            {
                Id = default,
                ProductId = 2,
                CustomerId = 1,                
                Status = OrderStatus.New,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            };

            using (var dbContext = _fixture.CreateDbContext())
            {
                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();
            }

            // Act
            await FluentActions.Invoking(() => _testClass.UpdateOrderAsync(invalidOrder, CancellationToken.None)).Should().ThrowAsync<ArgumentOutOfRangeException>().WithParameterName("updatedOrder.Id");

            // Assert
            using (var dbContext = _fixture.CreateDbContext())
            {
                dbContext.Orders.Should().BeEmpty();
            }
        }
    }
}