namespace LineTen.TechnicalTask.Domain.Models
{
    public class Product
    {
        private readonly string _name = null!;
        private readonly string _description = null!;
        private readonly string _sku = null!;

        public int Id { get; init; }

        public string Name
        {
            get => _name;
            init
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(nameof(Name));
                }

                _name = value;
            }
        }

        public string Description
        {
            get => _description;
            init
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(nameof(Description));
                }

                _description = value;
            }
        }

        public string SKU
        {
            get => _sku;
            init
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(nameof(SKU));
                }

                _sku = value;
            }
        }
    }
}