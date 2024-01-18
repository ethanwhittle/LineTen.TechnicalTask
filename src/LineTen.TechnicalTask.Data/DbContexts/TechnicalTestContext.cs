using LineTen.TechnicalTask.Data.Entities.Sql;
using Microsoft.EntityFrameworkCore;

namespace LineTen.TechnicalTask.Data.DbContexts
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.SpacingRules", "SA1009:Closing parenthesis should be spaced correctly", Justification = "Conflicting Static Analysis rule")]
    public class TechnicalTestContext(DbContextOptions<TechnicalTestContext> options) : DbContext(options)
    {
        public DbSet<CustomerEntity> Customers { get; set; } = null!;

        public DbSet<ProductEntity> Products { get; set; } = null!;

        public DbSet<OrderEntity> Orders { get; set; } = null!;
    }
}