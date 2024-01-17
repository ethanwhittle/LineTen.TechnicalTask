using LineTen.TechnicalTask.Domain.Models;

namespace LineTen.TechnicalTask.Domain.Tests.Models
{
    public class CustomerTests
    {
        private readonly Customer _testClass;
        private readonly int _id;
        private readonly string _firstName;
        private readonly string _lastName;
        private readonly string _phone;
        private readonly string _email;

        public CustomerTests()
        {
            _id = 858431650;
            _firstName = "TestValue938313762";
            _lastName = "TestValue577519715";
            _phone = "TestValue244194878";
            _email = "TestValue875672031";
            _testClass = new Customer
            {
                Id = _id,
                FirstName = _firstName,
                LastName = _lastName,
                Phone = _phone,
                Email = _email
            };
        }

        [Fact]
        public void CanConstruct()
        {
            // Act
            var instance = new Customer();

            // Assert
            instance.Should().NotBeNull();
        }

        [Fact]
        public void CanInitialize()
        {
            // Act
            var instance = new Customer
            {
                Id = _id,
                FirstName = _firstName,
                LastName = _lastName,
                Phone = _phone,
                Email = _email
            };

            // Assert
            instance.Should().NotBeNull();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void CannotInitializeWithInvalidFirstName(string value)
        {
            FluentActions.Invoking(() => new Customer
            {
                Id = _id,
                FirstName = value,
                LastName = _lastName,
                Phone = _phone,
                Email = _email
            }).Should().Throw<ArgumentNullException>().WithParameterName("FirstName");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void CannotInitializeWithInvalidLastName(string value)
        {
            FluentActions.Invoking(() => new Customer
            {
                Id = _id,
                FirstName = _firstName,
                LastName = value,
                Phone = _phone,
                Email = _email
            }).Should().Throw<ArgumentNullException>().WithParameterName("LastName");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void CannotInitializeWithInvalidPhone(string value)
        {
            FluentActions.Invoking(() => new Customer
            {
                Id = _id,
                FirstName = _firstName,
                LastName = _lastName,
                Phone = value,
                Email = _email
            }).Should().Throw<ArgumentNullException>().WithParameterName("Phone");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void CannotInitializeWithInvalidEmail(string value)
        {
            FluentActions.Invoking(() => new Customer
            {
                Id = _id,
                FirstName = _firstName,
                LastName = _lastName,
                Phone = _phone,
                Email = value
            }).Should().Throw<ArgumentNullException>().WithParameterName("Email");
        }

        [Fact]
        public void IdIsInitializedCorrectly()
        {
            _testClass.Id.Should().Be(_id);
        }

        [Fact]
        public void FirstNameIsInitializedCorrectly()
        {
            _testClass.FirstName.Should().Be(_firstName);
        }

        [Fact]
        public void LastNameIsInitializedCorrectly()
        {
            _testClass.LastName.Should().Be(_lastName);
        }

        [Fact]
        public void PhoneIsInitializedCorrectly()
        {
            _testClass.Phone.Should().Be(_phone);
        }

        [Fact]
        public void EmailIsInitializedCorrectly()
        {
            _testClass.Email.Should().Be(_email);
        }
    }
}