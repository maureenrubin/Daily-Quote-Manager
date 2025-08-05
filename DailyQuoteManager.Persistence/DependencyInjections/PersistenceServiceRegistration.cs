using DailyQuoteManager.Application.Contracts.Persistence;
using DailyQuoteManager.Persistence.DatabaseContext;
using DailyQuoteManager.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DailyQuoteManager.Persistence.DependencyInjections
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection")
                  ?? throw new ArgumentNullException("Database Connection String Is Missing!!!");

            services.AddHttpClient("ApiClient", client =>
            {
                // the running API's base URL + "api/"
                client.BaseAddress = new Uri("https://localhost:7223/api/");
            });


            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(connectionString)
                       .LogTo(Console.WriteLine, LogLevel.Information);
            });

            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));


            return services;
        }
      
    }
}
