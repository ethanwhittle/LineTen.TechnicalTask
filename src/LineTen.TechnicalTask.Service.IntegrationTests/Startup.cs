using LineTen.TechnicalTask.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LineTen.TechnicalTask.Service.IntegrationTests
{
    public class Startup
    {
        // ! States 0 references, but this is required by Xunits dependency injection
        public static void ConfigureServices(IServiceCollection services)
        {
            var configuration = new ConfigurationBuilder()
                //.SetBasePath(Directory.GetCurrentDirectory())
                //.AddJsonFile("appsettings.json", false, true)
                .AddEnvironmentVariables()
                .Build();

            services
                .AddSingleton<IConfiguration>(configuration)
                .AddDbContext<TechnicalTestContext>(options =>
                {
                    options.UseInMemoryDatabase(databaseName: "IntegrationTestsDatabase");
                });
        }
    }
}