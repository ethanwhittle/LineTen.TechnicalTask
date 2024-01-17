namespace LineTen.TechnicalTask.Domain.Models
{
    public class Customer
    {
        private readonly string _firstName = null!;
        private readonly string _lastName = null!;
        private readonly string _phone = null!;
        private readonly string _email = null!;

        public int Id { get; init; }

        public string FirstName
        {
            get => _firstName;
            init
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(nameof(FirstName));
                }

                _firstName = value;
            }
        }

        public string LastName
        {
            get => _lastName;
            init
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(nameof(LastName));
                }

                _lastName = value;
            }
        }

        public string Phone
        {
            get => _phone;
            init
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(nameof(Phone));
                }

                _phone = value;
            }
        }

        public string Email
        {
            get => _email;
            init
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(nameof(Email));
                }

                _email = value;
            }
        }
    }
}