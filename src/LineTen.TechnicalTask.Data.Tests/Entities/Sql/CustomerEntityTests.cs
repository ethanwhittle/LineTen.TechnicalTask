using System.ComponentModel.DataAnnotations;

namespace LineTen.TechnicalTask.Data.Tests.Entities.Sql
{
    public class CustomerEntity
    {
        // TODO: Navigation property

        [Key]
        public int Id { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public string Email { get; set; } = null!;

        public ICollection<OrderEntity> Orders { get; set; } = null!;
    }

    public class CustomerEntityTests
    {
        private readonly CustomerEntity _testClass;

        public CustomerEntityTests()
        {
            _testClass = new CustomerEntity();
        }

        [Fact]
        public void CanSetAndGetId()
        {
            // Arrange
            var testValue = 425807208;

            // Act
            _testClass.Id = testValue;

            // Assert
            _testClass.Id.Should().Be(testValue);
        }

        [Fact]
        public void CanSetAndGetFirstName()
        {
            // Arrange
            var testValue = "TestValue376717957";

            // Act
            _testClass.FirstName = testValue;

            // Assert
            _testClass.FirstName.Should().Be(testValue);
        }

        [Fact]
        public void CanSetAndGetLastName()
        {
            // Arrange
            var testValue = "TestValue622375035";

            // Act
            _testClass.LastName = testValue;

            // Assert
            _testClass.LastName.Should().Be(testValue);
        }

        [Fact]
        public void CanSetAndGetPhone()
        {
            // Arrange
            var testValue = "TestValue1570917759";

            // Act
            _testClass.Phone = testValue;

            // Assert
            _testClass.Phone.Should().Be(testValue);
        }

        [Fact]
        public void CanSetAndGetEmail()
        {
            // Arrange
            var testValue = "TestValue163790200";

            // Act
            _testClass.Email = testValue;

            // Assert
            _testClass.Email.Should().Be(testValue);
        }

        [Fact]
        public void CanSetAndGetOrders()
        {
            // Arrange
            var testValue = Substitute.For<ICollection<OrderEntity>>();

            // Act
            _testClass.Orders = testValue;

            // Assert
            _testClass.Orders.Should().BeSameAs(testValue);
        }
    }
}