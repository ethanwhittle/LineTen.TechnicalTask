using System.ComponentModel.DataAnnotations;

namespace LineTen.TechnicalTask.Service.Domain.Models
{
    public class AddProductRequest
    {
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = "SKU is required.")]
        public string SKU { get; set; } = null!;
    }
}