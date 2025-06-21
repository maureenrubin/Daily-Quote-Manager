using DailyQuoteManager.Client.InterfacesClient.Auth;
using DailyQuoteManager.Client.DTOs.Auth.Login;
using DailyQuoteManager.Client.Security;
using Microsoft.AspNetCore.Components;
using DailyQuoteManager.Client.Common.Responses;
using DailyQuoteManager.Client.Utilities;
using System.Security.Claims;

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
                    var authState = await customAuthStateProvider.GetAuthenticationStateAsync();
                    var user = authState.User;

                    var roleClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
                    var role = roleClaim?.Value ?? string.Empty;
                   
                    if(role == "User")
                    {
                        Navigation.NavigateTo("/dashboard");
                    }
                    else
                    {
                        Response = Response with { ErrorMessage = "Role is not recognized." };
                    }
                }
                else
                {
                    Logger.LogWarning("Login faild: Invalid credentials.");
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

        protected void ShowPasswordOnClick()
        {
            showPasswordUtil.Toggle();
        }

        #endregion
    }
}

