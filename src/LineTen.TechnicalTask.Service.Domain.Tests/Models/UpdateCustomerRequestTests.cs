using System.ComponentModel.DataAnnotations;

namespace LineTen.TechnicalTask.Service.Domain.Tests.Models
{
    public class UpdateCustomerRequest
    {
        [Required(ErrorMessage = "The customer Id is required.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "Last Name is required.")]
        public string LastName { get; set; } = null!;

        [Required(ErrorMessage = "Phone is required.")]
        [Phone(ErrorMessage = "Invalid phone number.")]
        public string Phone { get; set; } = null!;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; } = null!;
    }

    public class UpdateCustomerRequestTests
    {
        private readonly UpdateCustomerRequest _testClass;

        public UpdateCustomerRequestTests()
        {
            _testClass = new UpdateCustomerRequest();
        }

        [Fact]
        public void CanSetAndGetId()
        {
            // Arrange
            var testValue = 1302664855;

            // Act
            _testClass.Id = testValue;

            // Assert
            _testClass.Id.Should().Be(testValue);
        }

        [Fact]
        public void CanSetAndGetFirstName()
        {
            // Arrange
            var testValue = "TestValue1418323632";

            // Act
            _testClass.FirstName = testValue;

            // Assert
            _testClass.FirstName.Should().Be(testValue);
        }

        [Fact]
        public void CanSetAndGetLastName()
        {
            // Arrange
            var testValue = "TestValue1180647977";

            // Act
            _testClass.LastName = testValue;

            // Assert
            _testClass.LastName.Should().Be(testValue);
        }

        [Fact]
        public void CanSetAndGetPhone()
        {
            // Arrange
            var testValue = "TestValue829946918";

            // Act
            _testClass.Phone = testValue;

            // Assert
            _testClass.Phone.Should().Be(testValue);
        }

        [Fact]
        public void CanSetAndGetEmail()
        {
            // Arrange
            var testValue = "TestValue1063187194";

            // Act
            _testClass.Email = testValue;

            // Assert
            _testClass.Email.Should().Be(testValue);
        }
    }
}