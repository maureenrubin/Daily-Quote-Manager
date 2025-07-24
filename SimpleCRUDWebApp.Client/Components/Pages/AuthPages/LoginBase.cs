using DailyQuoteManager.Client.Common.Responses;
using DailyQuoteManager.Client.DTOs.Auth.Login;
using DailyQuoteManager.Client.InterfacesClient.Auth;
using DailyQuoteManager.Client.Security;
using DailyQuoteManager.Client.ServicesClient.Auth;
using DailyQuoteManager.Client.Utilities;
using Microsoft.AspNetCore.Components;
using System.Security.Claims;

namespace DailyQuoteManager.Client.Components.Pages.AuthPages
{
    public class LoginBase : ComponentBase
    {
        #region Injected Services
        [Inject] protected IAuthClientService AuthClientService { get; set; } = default!;
        [Inject] protected ITokenClientService TokenService { get; set; } = default!;
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
                    var token = await TokenService.GetToken();
                    if (!string.IsNullOrWhiteSpace(token))
                    {
                        await customAuthStateProvider.NotifyUserAuthentication();
                        var authState = await customAuthStateProvider.GetAuthenticationStateAsync();
                        var user = authState.User;

                        var roleClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
                        var role = roleClaim?.Value ?? string.Empty;

                        Logger.LogInformation($"Logged in user with role: {role}");

                        if (role == "DefaultUser")
                        {
                            Navigation.NavigateTo("/dashboard", forceLoad: true);
                        }
                        else
                        {
                            Response = Response with { ErrorMessage = "You do not have permission to access this system." };
                        }
                    }
                    else
                    {
                        Response = Response with { ErrorMessage = "Login succeeded, but token was not stored." };
                        Logger.LogWarning("Login succeeded but token is missing from localStorage.");
                    }
                }
                else
                {
                    Response = new Response(ErrorMessage: "Invalid email or password", MessageType: "warning");
                }
            }
            catch (Exception ex)
            {
                Response = Response with { ErrorMessage = "An error occurred while processing your request." };
                Logger.LogError(ex, "Error during login attempt");
            }
            finally
            {
                isLoading = false;
            }
        }

        protected void ShowPasswordOnClick()
        {
            showPasswordUtil.Toggle();
        }
        #endregion
    }
}
