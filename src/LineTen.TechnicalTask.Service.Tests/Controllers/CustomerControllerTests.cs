using AutoMapper;
using Castle.Core.Logging;
using LineTen.TechnicalTask.Domain.Models;
using LineTen.TechnicalTask.Service.Controllers;
using LineTen.TechnicalTask.Service.Domain.Mappings;
using LineTen.TechnicalTask.Service.Domain.Models;
using LineTen.TechnicalTask.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LineTen.TechnicalTask.Service.Tests.Controllers
{
    public class CustomerControllerTests
    {
        private readonly CustomerController _testClass;
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;
        private readonly ILogger<CustomerController> _logger;

        public CustomerControllerTests()
        {
            _customerService = Substitute.For<ICustomerService>();
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<RequestModelProfile>();
                cfg.AddProfile<ResponseModelProfile>();
            }).CreateMapper();

            _logger = Substitute.For<ILogger<CustomerController>>();

            _testClass = new CustomerController(_customerService, _mapper, _logger);
        }

        [Fact]
        public void CanConstruct()
        {
            // Act
            var instance = new CustomerController(_customerService, _mapper, _logger);

            // Assert
            instance.Should().NotBeNull();
        }

        [Fact]
        public void CannotConstructWithNullCustomerService()
        {
            FluentActions.Invoking(() => new CustomerController(default(ICustomerService), _mapper, _logger)).Should().Throw<ArgumentNullException>().WithParameterName("customerService");
        }

        [Fact]
        public void CannotConstructWithNullMapper()
        {
            FluentActions.Invoking(() => new CustomerController(_customerService, default(IMapper), _logger)).Should().Throw<ArgumentNullException>().WithParameterName("mapper");
        }

        [Fact]
        public void CannotConstructWithNullLogger()
        {
            FluentActions.Invoking(() => new CustomerController(_customerService, _mapper, default(ILogger<CustomerController>))).Should().Throw<ArgumentNullException>().WithParameterName("logger");
        }

        [Fact]
        public async Task CanCallAddCustomerAsync()
        {
            // Arrange
            var addCustomerRequest = new AddCustomerRequest
            {
                FirstName = "TestValue1500029421",
                LastName = "TestValue1226292970",
                Phone = "TestValue856336779",
                Email = "TestValue383542480"
            };

            var newCustomer = new Customer
            {
                Id = 1,
                FirstName = "TestValue1500029421",
                LastName = "TestValue1226292970",
                Phone = "TestValue856336779",
                Email = "TestValue383542480"
            };

            _customerService.AddCustomerAsync(Arg.Any<Customer>(), Arg.Any<CancellationToken>()).Returns(newCustomer);

            // Act
            var actionResult = await _testClass.AddCustomerAsync(addCustomerRequest, CancellationToken.None) as OkObjectResult;
            var result = actionResult!.Value as CustomerResponse;

            // Assert
            await _customerService.Received(1).AddCustomerAsync(Arg.Any<Customer>(), Arg.Any<CancellationToken>());

            result.Should().BeEquivalentTo(newCustomer).And.BeEquivalentTo(addCustomerRequest);
        }

        [Fact]
        public async Task CanCallGetAllCustomersAsync()
        {
            // Arrange
            var customers = Substitute.For<ICollection<Customer>>();

            _customerService.GetAllCustomersAsync(Arg.Any<CancellationToken>())
                .Returns(customers);

            // Act
            var actionResult = await _testClass.GetAllCustomersAsync(CancellationToken.None) as OkObjectResult;
            var result = actionResult!.Value as ICollection<CustomerResponse>;

            // Assert
            await _customerService.Received().GetAllCustomersAsync(Arg.Any<CancellationToken>());

            result.Should().BeEquivalentTo(customers);
        }

        [Fact]
        public async Task CanCallGetCustomerAsync()
        {
            // Arrange
            var id = 388898791;
            var customer = new Customer
            {
                Id = 388898791,
                FirstName = "TestValue42787513",
                LastName = "TestValue69599680",
                Phone = "TestValue1245200447",
                Email = "TestValue107379975"
            };

            _customerService.GetCustomerAsync(id, Arg.Any<CancellationToken>()).Returns(customer);

            // Act
            var actionResult = await _testClass.GetCustomerAsync(id, CancellationToken.None) as OkObjectResult;
            var result = actionResult!.Value as CustomerResponse;

            // Assert
            await _customerService.Received().GetCustomerAsync(id, Arg.Any<CancellationToken>());

            result.Should().BeEquivalentTo(customer);
        }

        [Fact]
        public async Task CannotCallGetCustomerAsyncWithInvalidId()
        {
            // Arrange
            var invalidId = default(int);

            // Act
            var actionResult = await _testClass.GetCustomerAsync(invalidId, CancellationToken.None) as BadRequestObjectResult;

            // Assert
            actionResult.Should().NotBeNull();
            actionResult!.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        [Fact]
        public async Task CannotCallGetCustomerAsyncWithNonexistentId()
        {
            // Arrange
            var nonExistentId = 1;

            _customerService.GetCustomerAsync(nonExistentId, Arg.Any<CancellationToken>()).Returns(default(Customer?));

            // Act
            var actionResult = await _testClass.GetCustomerAsync(nonExistentId, CancellationToken.None) as NotFoundResult;

            // Assert
            actionResult.Should().NotBeNull();
            actionResult!.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }

        [Fact]
        public async Task CanCallUpdateCustomerAsync()
        {
            // Arrange
            var updateCustomerRequest = new UpdateCustomerRequest
            {
                Id = 424208094,
                FirstName = "TestValue630435485",
                LastName = "TestValue939603568",
                Phone = "TestValue328096136",
                Email = "TestValue597415858"
            };

            var customer = new Customer
            {
                Id = 424208094,
                FirstName = "TestValue630435485",
                LastName = "TestValue939603568",
                Phone = "TestValue328096136",
                Email = "TestValue597415858"
            };

            var cancellationToken = CancellationToken.None;

            _customerService.UpdateCustomerAsync(Arg.Any<Customer>(), Arg.Any<CancellationToken>()).Returns(customer);

            // Act
            var actionResult = await _testClass.UpdateCustomerAsync(updateCustomerRequest, cancellationToken) as OkObjectResult;
            var result = actionResult!.Value as CustomerResponse;

            // Assert
            await _customerService.Received().UpdateCustomerAsync(Arg.Any<Customer>(), Arg.Any<CancellationToken>());

            result.Should().BeEquivalentTo(customer).And.BeEquivalentTo(updateCustomerRequest);
        }

        [Fact]
        public async Task CanCallDeleteCustomerAsync()
        {
            // Arrange
            var id = 1000729004;
            var cancellationToken = CancellationToken.None;

            _customerService.DeleteCustomerAsync(id, Arg.Any<CancellationToken>()).Returns(true);

            // Act
            var actionResult = await _testClass.DeleteCustomerAsync(id, cancellationToken);

            // Assert
            await _customerService.Received(1).DeleteCustomerAsync(id, Arg.Any<CancellationToken>());

            actionResult.Should().BeOfType<OkResult>();
        }

        [Fact]
        public async Task CannotCallDeleteCustomerAsyncWithNonexistentId()
        {
            // Arrange
            var nonExistentId = 1;

            _customerService.DeleteCustomerAsync(nonExistentId, Arg.Any<CancellationToken>()).Returns(false);

            // Act
            var actionResult = await _testClass.DeleteCustomerAsync(nonExistentId, CancellationToken.None) as NotFoundResult;

            // Assert
            actionResult.Should().NotBeNull();
            actionResult!.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }
    }
}