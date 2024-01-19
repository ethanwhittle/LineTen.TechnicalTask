using LineTen.TechnicalTask.Data.DbContexts;
using LineTen.TechnicalTask.Data.Mappings;
using LineTen.TechnicalTask.Data.Repositories;
using LineTen.TechnicalTask.Data.Repositories.Sql;
using LineTen.TechnicalTask.Service.Domain.Mappings;
using LineTen.TechnicalTask.Service.Services;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.EntityFrameworkCore;

namespace LineTen.TechnicalTask.Service
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            ArgumentNullException.ThrowIfNull(configuration);

            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services
                .AddDbContext<TechnicalTestContext>(options =>
                {
                    options.UseSqlServer(Configuration.GetConnectionString("TechnicalTestDatabase"));
                    options.EnableServiceProviderCaching(false);
                    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                    options.EnableSensitiveDataLogging(true);
                })
                .AddScoped<ICustomerRepository, SqlCustomerRepository>()
                .AddScoped<IProductRepository, SqlProductRepository>()
                .AddScoped<IOrderRepository, SqlOrderRepository>()
                .AddScoped<ICustomerService, CustomerService>()
                .AddScoped<IProductService, ProductService>()
                .AddScoped<IOrderService, OrderService>()
                .AddAutoMapper(cfg =>
                {
                    cfg.AddProfile<EntityModelProfile>();
                    cfg.AddProfile<RequestModelProfile>();
                    cfg.AddProfile<ResponseModelProfile>();
                });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            ArgumentNullException.ThrowIfNull(app);
            ArgumentNullException.ThrowIfNull(env);

            if (env.IsDevelopment())
            {
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<TechnicalTestContext>();
                    
                    // Drops and recreates the local database on each app launch
                    if (context.Database.CanConnect())
                    {
                        context.Database.EnsureDeleted();
                    }

                    context.Database.EnsureCreated();
                }

                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TechnicalTask Service"));
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors(builder =>
            {
                builder
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });

            app.UseCookiePolicy(
                new CookiePolicyOptions
                {
                    HttpOnly = HttpOnlyPolicy.Always,
                    Secure = CookieSecurePolicy.Always
                });

            // Configure your middleware here
            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}