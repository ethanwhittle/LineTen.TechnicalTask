using System.ComponentModel.DataAnnotations;

namespace LineTen.TechnicalTask.Data.Entities.Sql
{
    public class CustomerEntity
    {
        [Key]
        public int Id { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public string Email { get; set; } = null!;

        public ICollection<OrderEntity> Orders { get; set; } = null!;
    }
}