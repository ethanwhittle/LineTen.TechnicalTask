using LineTen.TechnicalTask.Data.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace LineTen.TechnicalTask.Data.Tests.Fixtures
{
    public class TechnicalTestContextFixture : IDisposable
    {
        public DbContextOptions<TechnicalTestContext> DbContextOptions { get; }

        public TechnicalTestContextFixture()
        {
            DbContextOptions = new DbContextOptionsBuilder<TechnicalTestContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryTechnicalTestDatabase")
                .Options;

            using var dbContext = new TechnicalTestContext(DbContextOptions);
            dbContext.Database.EnsureCreated();
        }

        public TechnicalTestContext CreateDbContext()
        {
            return new TechnicalTestContext(DbContextOptions);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                using var dbContext = new TechnicalTestContext(DbContextOptions);
                dbContext.Database.EnsureDeleted();
            }
        }
    }
}