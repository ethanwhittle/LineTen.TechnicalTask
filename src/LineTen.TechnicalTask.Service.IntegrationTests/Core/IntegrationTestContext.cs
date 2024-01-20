using LineTen.TechnicalTask.Data.DbContexts;
using Microsoft.AspNetCore.Mvc.Testing;

namespace LineTen.TechnicalTask.Service.IntegrationTests.Core
{
    public class IntegrationTestContext : IDisposable
    {
        public TechnicalTestContext DbContext { get; }

        public TechnicalTaskWebApplicationFactory Factory { get; }

        public HttpClient HttpClient { get; }

        public IntegrationTestContext(TechnicalTestContext dbContext)
        {
            DbContext = dbContext;

            Factory = new TechnicalTaskWebApplicationFactory();

            HttpClient = Factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                BaseAddress = new Uri("http://localhost/api")
            });
        }

        public void Dispose()
        {
            HttpClient.Dispose();
            Factory.Dispose();
        }
    }
}