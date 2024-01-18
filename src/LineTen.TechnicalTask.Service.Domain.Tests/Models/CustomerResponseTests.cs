namespace LineTen.TechnicalTask.Service.Domain.Tests.Models
{
    public class CustomerResponse
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public string Email { get; set; } = null!;
    }

    public class CustomerResponseTests
    {
        private readonly CustomerResponse _testClass;

        public CustomerResponseTests()
        {
            _testClass = new CustomerResponse();
        }

        [Fact]
        public void CanSetAndGetId()
        {
            // Arrange
            var testValue = 21943411;

            // Act
            _testClass.Id = testValue;

            // Assert
            _testClass.Id.Should().Be(testValue);
        }

        [Fact]
        public void CanSetAndGetFirstName()
        {
            // Arrange
            var testValue = "TestValue1822828591";

            // Act
            _testClass.FirstName = testValue;

            // Assert
            _testClass.FirstName.Should().Be(testValue);
        }

        [Fact]
        public void CanSetAndGetLastName()
        {
            // Arrange
            var testValue = "TestValue1230124804";

            // Act
            _testClass.LastName = testValue;

            // Assert
            _testClass.LastName.Should().Be(testValue);
        }

        [Fact]
        public void CanSetAndGetPhone()
        {
            // Arrange
            var testValue = "TestValue1961007568";

            // Act
            _testClass.Phone = testValue;

            // Assert
            _testClass.Phone.Should().Be(testValue);
        }

        [Fact]
        public void CanSetAndGetEmail()
        {
            // Arrange
            var testValue = "TestValue152819838";

            // Act
            _testClass.Email = testValue;

            // Assert
            _testClass.Email.Should().Be(testValue);
        }
    }
}