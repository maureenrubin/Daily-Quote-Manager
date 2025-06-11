using DailyQuoteManager.Application.Services;
using DailyQuoteManager.Application.Services.Interfaces;
using DailyQuoteManager.Domain.Interfaces;
using DailyQuoteManager.Infrastructure.Data;
using DailyQuoteManager.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DailyQuoteManager.Infrastructure.DependencyInjections
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            AddPersistence(services, configuration);
            return services;
        }

        private static void AddPersistence(IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection")!;

            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException(nameof(configuration), "Database Connection String Is Missing");

            services.AddHttpClient("ApiClient", client =>
            {
                // <-- Set this to your running API's base URL + "api/"
                client.BaseAddress = new Uri("https://localhost:7223/api/");
            });

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(connectionString)
                       .LogTo(Console.WriteLine, LogLevel.Information);
            });

            //Repository
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();


            //Services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
        
        
        
        }
    }
}
