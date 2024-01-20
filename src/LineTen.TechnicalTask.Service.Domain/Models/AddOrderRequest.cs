using LineTen.TechnicalTask.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace LineTen.TechnicalTask.Service.Domain.Models
{
    public class AddOrderRequest
    {
        [Required(ErrorMessage = "ProductId is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Id must be greater than zero.")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "CustomerId is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Id must be greater than zero.")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        [EnumDataType(typeof(OrderStatus), ErrorMessage = "Invalid OrderStatus value.")]
        public OrderStatus Status { get; set; }
    }
}