using DailyQuoteManager.Client.Common.Constants;
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
        [Inject] protected NavigationManager NavigateManager { get; set; } = default!;
        [Inject] protected ILogger<LoginBase> Logger { get; set; } = default!;
       
        #endregion

        #region Protected Fields
        protected LoginInputRequestDto Input { get; set; } = new();
        protected Response Response { get; set; } = new();
        protected ShowPasswordUtil showPasswordUtil { get; set; } = new();
        protected bool isLoading { get; set; } = false;
        protected string _testResult;

        #endregion

        #region Protected Methods

      
        protected async Task HandleLogicClick()
        {
            try
            {
                var isAuthenticated = await AuthClientService.LoginAsync(Input.Email, Input.Password);

                if (isAuthenticated)
                {
                    await customAuthStateProvider.NotifyUserAuthentication();
                    var authState = await customAuthStateProvider.GetAuthenticationStateAsync();
                    var user = authState.User;

                    var roleClaim = user.Claims.FirstOrDefault(c => c.Type == RoleConstants.Role || c.Type == ClaimTypes.Role);
                    var role = roleClaim?.Value ?? string.Empty;

                    if (role == RoleConstants.Admin)
                    {
                        NavigateManager.NavigateTo("/adminDashboard", forceLoad: true);
                    }
                    else if (role == RoleConstants.DefaultUser)
                    {
                        NavigateManager.NavigateTo("/userDashboard", forceLoad: true);
                    }
                    else
                    {
                        Response = Response with { ErrorMessage = "Role is not recognized" };
                    }
                }
                else
                {
                    Response = Response with { ErrorMessage = "Invalid login credentials." };
                }
            }
            catch
            {
                Response = Response with { ErrorMessage = "An error occured while processing your request." };
             }
        }


        #endregion
    }
}
