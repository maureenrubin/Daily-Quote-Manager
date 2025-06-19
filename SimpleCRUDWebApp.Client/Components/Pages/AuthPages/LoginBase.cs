using DailyQuoteManager.Client.InterfacesClient.Auth;
using Microsoft.AspNetCore.Components;

namespace DailyQuoteManager.Client.Components.Pages.AuthPages
{
    public class LoginBase : ComponentBase
    {
        #region Injected Services
        [Inject] protected IAuthClientService AuthClientService { get; set; } = default!;
        [Inject] protected NavigationManager Navigation { get; set; } = default!;
        [Inject] protected ILogger<LoginBase> Logger { get; set; } = default!;
        #endregion

        #region Protected Fields
        protected string email = string.Empty;
        protected string password = string.Empty;
        protected string errorMessage = string.Empty;
        protected bool isLoading = false;
        #endregion

        #region Protected Methods
        protected async Task HandleLogin()
        {
            try
            {
                isLoading = true;
                errorMessage = string.Empty;
                StateHasChanged();

                var success = await AuthClientService.LoginAsync(email, password);

                if (success)
                {
                    Logger.LogInformation($"User {email} logged in successfully");

                    // Redirect to the next page after successful login
                    Navigation.NavigateTo("/dashboard", true); 
                }
                else
                {
                    errorMessage = "Invalid email or password. Please try again.";
                    Logger.LogWarning($"Failed login attempt for email: {email}");
                }
            }
            catch (Exception ex)
            {
                errorMessage = "An error occurred during login. Please try again.";
                Logger.LogError(ex, $"Login error for email: {email}");
            }
            finally
            {
                isLoading = false;
                StateHasChanged();
            }
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }
        #endregion
    }
}

