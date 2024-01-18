using LineTen.TechnicalTask.Domain.Enums;

namespace LineTen.TechnicalTask.Service.Domain.Models
{
    public class OrderResponse
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int CustomerId { get; set; }

        public OrderStatus Status { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}