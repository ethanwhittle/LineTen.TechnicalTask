using System.ComponentModel.DataAnnotations;

namespace LineTen.TechnicalTask.Data.Entities.Sql
{
    public class ProductEntity
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string SKU { get; set; } = null!;

        public ICollection<OrderEntity> Orders { get; set; } = null!;
    }
}