using System.ComponentModel.DataAnnotations;

namespace LineTen.TechnicalTask.Data.Entities.Sql
{
    public class OrderEntity
    {
        [Key]
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int CustomerId { get; set; }

        public int Status { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public ProductEntity Product { get; set; } = null!;

        public CustomerEntity Customer { get; set; } = null!;
    }
}