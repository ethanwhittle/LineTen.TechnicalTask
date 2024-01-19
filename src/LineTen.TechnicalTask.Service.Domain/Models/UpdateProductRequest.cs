using System.ComponentModel.DataAnnotations;

namespace LineTen.TechnicalTask.Service.Domain.Models
{
    public class UpdateProductRequest
    {
        [Required(ErrorMessage = "Id is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Id must be greater than zero.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = "SKU is required.")]
        public string SKU { get; set; } = null!;
    }
}