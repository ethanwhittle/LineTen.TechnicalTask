using LineTen.TechnicalTask.Domain.Enums;

namespace LineTen.TechnicalTask.Domain.Models
{
    public class Order
    {
        public int ProductId { get; init; }

        public int CustomerId { get; init; }

        public OrderStatus Status { get; init; }

        public DateTime CreatedDate { get; init; }

        public DateTime UpdatedDate { get; init; }
    }
}