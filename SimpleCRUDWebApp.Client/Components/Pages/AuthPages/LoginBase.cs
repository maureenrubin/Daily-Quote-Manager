using DailyQuoteManager.Client.InterfacesClient.Auth;
using DailyQuoteManager.Client.DTOs.Auth.Login;
using DailyQuoteManager.Client.Security;
using Microsoft.AspNetCore.Components;
using DailyQuoteManager.Client.Common.Responses;
using DailyQuoteManager.Client.Utilities;

namespace DailyQuoteManager.Client.Components.Pages.AuthPages
{
    public class LoginBase : ComponentBase
    {
        #region Injected Services
        [Inject] protected IAuthClientService AuthClientService { get; set; } = default!;
        [Inject] protected CustomAuthStateProvider customAuthStateProvider { get; set; } = default!;
        [Inject] protected NavigationManager Navigation { get; set; } = default!;
        [Inject] protected ILogger<LoginBase> Logger { get; set; } = default!;

        #endregion

        #region Protected Fields
        protected LoginInputRequestDto Input { get; set; } = new();
        protected Response Response { get; set; } = new();
        protected ShowPasswordUtil showPasswordUtil { get; set; } = new();
        protected bool isLoading { get; set; } = false;
        #endregion

        #region Protected Methods
        protected async Task HandleLoginOnClick()
        {
            isLoading = true;

            try
            {
                var loginSuccess = await AuthClientService.LoginAsync(Input.Email, Input.Password);

                if (loginSuccess)
                {
                    await customAuthStateProvider.NotifyUserAuthenticationStateChange();
                    Navigation.NavigateTo("/dashboard");
                }
                else
                {
                    Logger.LogWarning("Login failde: Invalid credentials.");
                    Response = new Response(ErrorMessage: "Invalid email or password", MessageType: "warning");
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "An unexpected error occurred during login.");
                Response = new Response(ErrorMessage: "Unexpected error. Please try again later.", MessageType: "error");
            }
            finally
            {
                isLoading = false;
            }

        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }
        #endregion
    }
}

