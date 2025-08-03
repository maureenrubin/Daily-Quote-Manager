using DailyQuoteManager.Application.Common.Mapping;
using DailyQuoteManager.Application.Contracts.Interfaces.Auth;
using DailyQuoteManager.Application.Contracts.Interfaces.Quote;
using DailyQuoteManager.Application.Contracts.Interfaces.UserManagement;
using DailyQuoteManager.Application.Services.Auth;
using DailyQuoteManager.Application.Services.Quote;
using DailyQuoteManager.Application.Services.UserManagement;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DailyQuoteManager.Application.DependencyInjections
{
    public static class ApplicationServiceRegistration
    {
        #region Public Methods

        public static IServiceCollection AddApplicationService(
            this IServiceCollection service, IConfiguration configuration)
        {

            service.AddAutoMapper(typeof(BaseMappingProfiles).Assembly);

            service.AddScoped<IUserService, UserService>();
            service.AddScoped<IAuthService, AuthService>();
            service.AddScoped<ITokenService, TokenService>();
            service.AddScoped<IPasswordHasherService, PasswordHasherService>();
            service.AddScoped<IRefreshTokenService, RefreshTokenService>();
            service.AddScoped<IQuoteService, QuoteService>();

            return service;
        }

        #endregion Public Methods



    }
}
