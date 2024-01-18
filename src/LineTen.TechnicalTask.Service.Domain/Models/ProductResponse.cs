namespace LineTen.TechnicalTask.Service.Domain.Models
{
    public class ProductResponse
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string SKU { get; set; } = null!;
    }
}