using DailyQuoteManager.Client.Common.Events;
using DailyQuoteManager.Client.InterfacesClient.Auth;
using DailyQuoteManager.Client.InterfacesClient.Quote;
using DailyQuoteManager.Client.InterfacesClient.UserManagement;
using DailyQuoteManager.Client.Security;
using DailyQuoteManager.Client.ServicesClient.Auth;
using DailyQuoteManager.Client.ServicesClient.Quote;
using DailyQuoteManager.Client.ServicesClient.UserManagement;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor.Services;

namespace DailyQuoteManager.Client.DependencyInjections
{
    public static class DependencyInjection
    {
        #region Public Methods

        public static IServiceCollection AddServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            AddPersistence(services, configuration);

            return services;
        }

        #endregion Public Methods 


        #region Private Methods 
        
        private static void AddPersistence(IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient("ApiClient", client =>
            {
                client.BaseAddress = new Uri("https://localhost:7223/api/");
                client.Timeout = TimeSpan.FromMinutes(2);
            }).AddHttpMessageHandler<JwtHttpMessageHandler>();

            services.ConfigureApplicationCookie(options =>
            {
                options.EventsType = typeof(CookieEvents);
            });


            services.AddAuthentication()
            .AddScheme<CustomOption, JWTAuthenticationCookieHandler>("JWTAuth", options => { });
            
            services.AddMudServices();
            services.AddCascadingAuthenticationState();
            services.AddHttpContextAccessor();

            services.AddRazorComponents()
                .AddInteractiveServerComponents();


            services.AddScoped<IAccountManagementClientService, AccountManagementClientService>();
            services.AddScoped<IAuthClientService, AuthClientService>();
            services.AddScoped<IQuoteClientService, QuoteClientService>();
            services.AddScoped<IUserClientService, UserClientService>();
            services.AddScoped<ITokenClientService, TokenClientService>();
            services.AddScoped<IRefreshTokenClientService, RefreshTokenClientService>();
            services.AddScoped<ICookieClientService, CookieClientService>();
            services.AddScoped<JwtHttpMessageHandler>();
            services.AddScoped<CustomAuthStateProvider>();
            services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

        }

        #endregion Private Methods 



    }
}
