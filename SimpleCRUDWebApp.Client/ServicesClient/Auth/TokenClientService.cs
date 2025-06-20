using DailyQuoteManager.Client.InterfacesClient.Auth;
using Microsoft.JSInterop;
using Newtonsoft.Json.Linq;

namespace DailyQuoteManager.Client.ServicesClient.Auth
{
    public class TokenClientService(
        IJSRuntime jsRuntime,
        ICookieClientService cookiesServices): ITokenClientService
    {
        #region Fields

        private readonly string tokenKey = "access_token";

        #endregion Fields

        #region Public Methods

        public async Task SetToken(string accessToken)
        {
            try
            {
                await cookiesServices.SetCookies(tokenKey, accessToken, 1);
                await jsRuntime.InvokeVoidAsync("localStorage.setItem", tokenKey, accessToken);
            }
            catch(InvalidOperationException)
            {

            }
        }

        public async Task<string> GetToken()
        {
            string token = null;
            try
            {
                token = await jsRuntime.InvokeAsync<string>("localStorage.getItem");

            }
            catch (InvalidOperationException ex)
            {
                Console.Error.WriteLine($"[TokenService] Failed to get token from cookies: {ex.Message}"); 
            }

            if (string.IsNullOrEmpty(token))
            {
                try
                {
                    token = await jsRuntime.InvokeAsync<string>("localStorage.getItem", tokenKey);  
                }
                catch (InvalidOperationException ex)
                {
                    Console.Error.WriteLine($"[TokenService] Failed to get token from localStorage: {ex.Message}");
                }
            }
            
            return token;
        }

        public async Task RemoveToken()
        {
            try
            {
                await cookiesServices.RemoveCookies(tokenKey);
                await jsRuntime.InvokeVoidAsync("localStorage.removeItem", tokenKey);
            }
            catch (InvalidOperationException)
            {

            }
           
        }

        #endregion Public Methods


    }
}
    