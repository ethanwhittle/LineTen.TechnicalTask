using System.ComponentModel.DataAnnotations;

namespace LineTen.TechnicalTask.Data.Tests.Entities.Sql
{
    public class CustomerEntityTests
    {
        public class CustomerEntity
        {
            // TODO: Navigation property

            [Key]
            public int Id { get; init; }

            public string FirstName { get; init; } = null!;

            public string LastName { get; init; } = null!;

            public string Phone { get; init; } = null!;

            public string Email { get; init; } = null!;
        }

        private readonly CustomerEntity _testClass;
        private readonly int _id;
        private readonly string _firstName;
        private readonly string _lastName;
        private readonly string _phone;
        private readonly string _email;

        public CustomerEntityTests()
        {
            _id = 1495598628;
            _firstName = "TestValue585154243";
            _lastName = "TestValue923455381";
            _phone = "TestValue1736527465";
            _email = "TestValue2124148228";
            _testClass = new CustomerEntity
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
            var instance = new CustomerEntity();

            // Assert
            instance.Should().NotBeNull();
        }

        [Fact]
        public void CanInitialize()
        {
            // Act
            var instance = new CustomerEntity
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