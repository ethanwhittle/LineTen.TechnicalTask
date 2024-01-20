using LineTen.TechnicalTask.Service.Domain.Models;
using LineTen.TechnicalTask.Service.IntegrationTests.Core;
using System.Net;
using System.Net.Http.Json;

namespace LineTen.TechnicalTask.Service.IntegrationTests.Controllers
{
    [CollectionDefinition(nameof(IntegrationTestCollection))]
    public class CustomerControllerIntegrationTests : IClassFixture<IntegrationTestContext>
    {
        private readonly IntegrationTestContext _context;

        public CustomerControllerIntegrationTests(IntegrationTestContext context)
        {
            _context = context;

            // Ensure a clean state for each test case
            _context.DbContext.Database.EnsureDeleted();
            _context.DbContext.Database.EnsureCreated();
        }

        [Fact]
        public async Task AddCustomerAsync_ValidRequest_ReturnsOkResult()
        {
            // Arrange
            var addCustomerRequest = new AddCustomerRequest
            {
                FirstName = "Ethan",
                LastName = "Whittle",
                Phone = "+447525417714",
                Email = "ethanwhittle@hotmail.co.uk"
            };

            // Act
            var response = await _context.HttpClient.PostAsJsonAsync("/api/customers", addCustomerRequest);
            response.EnsureSuccessStatusCode();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var result = await response.Content.ReadFromJsonAsync<CustomerResponse>();
            result.Should().NotBeNull();
            result!.FirstName.Should().Be(addCustomerRequest.FirstName);
            result!.LastName.Should().Be(addCustomerRequest.LastName);
            result!.Phone.Should().Be(addCustomerRequest.Phone);
            result!.Email.Should().Be(addCustomerRequest.Email);
        }

        [Fact]
        public async Task AddCustomerAsync_InvalidRequest_ReturnsBadRequest()
        {
            // Arrange
            var addCustomerRequest = new AddCustomerRequest();

            // Act
            var response = await _context.HttpClient.PostAsJsonAsync("/api/customers", addCustomerRequest);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task GetAllCustomersAsync_ReturnsOkResult()
        {
            // Act
            var response = await _context.HttpClient.GetAsync("/api/customers");
            response.EnsureSuccessStatusCode();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var result = await response.Content.ReadFromJsonAsync<List<CustomerResponse>>();
            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }

        [Fact]
        public async Task GetCustomerAsync_ExistingId_ReturnsOkResult()
        {
            // Arrange
            var addCustomerRequest = new AddCustomerRequest
            {
                FirstName = "Ethan",
                LastName = "Whittle",
                Phone = "+447525417714",
                Email = "ethanwhittle@hotmail.co.uk"
            };

            // Act
            var addUserResponse = await _context.HttpClient.PostAsJsonAsync("/api/customers", addCustomerRequest);
            addUserResponse.EnsureSuccessStatusCode();

            var addedUser = await addUserResponse.Content.ReadFromJsonAsync<CustomerResponse>();

            addedUser.Should().NotBeNull();

            var getUserResponse = await _context.HttpClient.GetAsync($"/api/customers/{addedUser!.Id}");
            getUserResponse.EnsureSuccessStatusCode();

            // Assert
            getUserResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var result = await getUserResponse.Content.ReadFromJsonAsync<CustomerResponse>();
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(addedUser);
        }

        [Fact]
        public async Task GetCustomerAsync_InvalidId_ReturnsBadRequest()
        {
            // Arrange
            var invalidCustomerId = 0;

            // Act
            var response = await _context.HttpClient.GetAsync($"/api/customers/{invalidCustomerId}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task GetCustomerAsync_NonexistentId_ReturnsNotFound()
        {
            // Arrange
            var nonexistentCustomerId = 1;

            // Act
            var response = await _context.HttpClient.GetAsync($"/api/customers/{nonexistentCustomerId}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task UpdateCustomerAsync_ValidRequest_ReturnsOkResult()
        {
            // Arrange
            var addCustomerRequest = new AddCustomerRequest
            {
                FirstName = "Ethan",
                LastName = "Whittle",
                Phone = "+447525417714",
                Email = "ethanwhittle@hotmail.co.uk"
            };

            // Act
            var addUserResponse = await _context.HttpClient.PostAsJsonAsync("/api/customers", addCustomerRequest);
            addUserResponse.EnsureSuccessStatusCode();

            var addedUser = await addUserResponse.Content.ReadFromJsonAsync<CustomerResponse>();

            addedUser.Should().NotBeNull();

            var updateCustomerRequest = new UpdateCustomerRequest
            {
                Id = addedUser!.Id,
                FirstName = "Ethan",
                LastName = "Whittle",
                Phone = "+447525417718",
                Email = "ethanwhittle@test.com"
            };

            // Use PostAsJsonAsync for the update request
            var updateUserResponse = await _context.HttpClient.PutAsJsonAsync("/api/customers", updateCustomerRequest);
            updateUserResponse.EnsureSuccessStatusCode();

            var updatedUser = await updateUserResponse.Content.ReadFromJsonAsync<CustomerResponse>();

            // Assert
            updateUserResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            updatedUser.Should().NotBeNull().And.BeEquivalentTo(updateCustomerRequest);
        }

        [Fact]
        public async Task UpdateCustomerAsync_InvalidRequest_ReturnsBadRequest()
        {
            // Arrange
            var updateCustomerRequest = new UpdateCustomerRequest();

            // Act
            var response = await _context.HttpClient.PutAsJsonAsync("/api/customers", updateCustomerRequest);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task DeleteCustomerAsync_ExistingId_ReturnsOkResult()
        {
            // Arrange
            var addCustomerRequest = new AddCustomerRequest
            {
                FirstName = "Ethan",
                LastName = "Whittle",
                Phone = "+447525417714",
                Email = "ethanwhittle@hotmail.co.uk"
            };

            // Act
            var addUserResponse = await _context.HttpClient.PostAsJsonAsync("/api/customers", addCustomerRequest);
            addUserResponse.EnsureSuccessStatusCode();

            var addedUser = await addUserResponse.Content.ReadFromJsonAsync<CustomerResponse>();

            addedUser.Should().NotBeNull();

            var deleteUserResponse = await _context.HttpClient.DeleteAsync($"/api/customers/{addedUser!.Id}");
            deleteUserResponse.EnsureSuccessStatusCode();

            // Assert
            deleteUserResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task DeleteCustomerAsync_NonexistentId_ReturnsNotFound()
        {
            // Arrange
            var nonexistentCustomerId = 1;

            // Act
            var response = await _context.HttpClient.DeleteAsync($"/api/customers/{nonexistentCustomerId}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}