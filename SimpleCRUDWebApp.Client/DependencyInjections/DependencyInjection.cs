using DailyQuoteManager.Client.Services;
using DailyQuoteManager.Client.Services.Contracts;
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
                // <-- Set this to your running API's base URL + "api/"
                client.BaseAddress = new Uri("https://localhost:7223/api/");
            });

            services.AddMudServices();
            services.AddCascadingAuthenticationState();
            services.AddHttpContextAccessor();

            services.AddRazorComponents()
                .AddInteractiveServerComponents();


            services.AddScoped<IAccountManagementClientService, AccountManagementClientService>();
            services.AddScoped<IAuthClientService, AuthClientService>();
            services.AddScoped<IQuoteClientService, QuoteClientService>();
            services.AddScoped<ISnackbarService, SnackbarService>();
            services.AddScoped<IUserClientService, UserClientService>();
            services.AddScoped<ITokenClientService, TokenClientService>();
        }

        #endregion Private Methods 



    }
}
