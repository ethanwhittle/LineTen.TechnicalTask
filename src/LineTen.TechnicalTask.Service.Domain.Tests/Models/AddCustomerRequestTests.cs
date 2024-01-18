using System.ComponentModel.DataAnnotations;

namespace LineTen.TechnicalTask.Service.Domain.Tests.Models
{
    public class AddCustomerRequest
    {
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

    public class AddCustomerRequestTests
    {
        private readonly AddCustomerRequest _testClass;

        public AddCustomerRequestTests()
        {
            _testClass = new AddCustomerRequest();
        }

        [Fact]
        public void CanSetAndGetFirstName()
        {
            // Arrange
            var testValue = "TestValue1441141579";

            // Act
            _testClass.FirstName = testValue;

            // Assert
            _testClass.FirstName.Should().Be(testValue);
        }

        [Fact]
        public void CanSetAndGetLastName()
        {
            // Arrange
            var testValue = "TestValue493019195";

            // Act
            _testClass.LastName = testValue;

            // Assert
            _testClass.LastName.Should().Be(testValue);
        }

        [Fact]
        public void CanSetAndGetPhone()
        {
            // Arrange
            var testValue = "TestValue1716115395";

            // Act
            _testClass.Phone = testValue;

            // Assert
            _testClass.Phone.Should().Be(testValue);
        }

        [Fact]
        public void CanSetAndGetEmail()
        {
            // Arrange
            var testValue = "TestValue1345593422";

            // Act
            _testClass.Email = testValue;

            // Assert
            _testClass.Email.Should().Be(testValue);
        }
    }
}