﻿using DailyQuoteManager.Application.Interfaces.Auth;
using DailyQuoteManager.Application.Interfaces.UserManagement;
using DailyQuoteManager.Application.Services.Auth;
using DailyQuoteManager.Infrastructure.Persistence.DatabaseContext;
using DailyQuoteManager.Infrastructure.Persistence.UnitOfWork;
using DailyQuoteManager.Application.Services.UserManagement;
using DailyQuoteManager.Domain.Interfaces;
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
                // the running API's base URL + "api/"
                client.BaseAddress = new Uri("https://localhost:7223/api/");
            });


            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(connectionString)
                       .LogTo(Console.WriteLine, LogLevel.Information);
            });

            services.AddRepositories();

            //Services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IPasswordHasherService, PasswordHasherService>(); 
            services.AddScoped<IUnitOfWork, UnitOfWork>(); 



        }
    }
}
