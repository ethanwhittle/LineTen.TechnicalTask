using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace LineTen.TechnicalTask.Service.IntegrationTests.Core
{
    public class TechnicalTaskWebApplicationFactory : WebApplicationFactory<Service.Startup>
    {
        public const string HostEnvironment = "IntegrationTests";

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment(HostEnvironment);
        }
    }
}