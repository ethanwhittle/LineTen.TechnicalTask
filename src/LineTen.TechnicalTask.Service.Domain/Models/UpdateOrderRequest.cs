using LineTen.TechnicalTask.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace LineTen.TechnicalTask.Service.Domain.Models
{
    public class UpdateOrderRequest
    {
        [Required(ErrorMessage = "Id is required.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "ProductId is required.")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "CustomerId is required.")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        public OrderStatus Status { get; set; }
    }
}