using DailyQuoteManager.Client.InterfacesClient.Auth;
using Microsoft.JSInterop;

namespace DailyQuoteManager.Client.ServicesClient.Auth
{
    public class TokenClientService(
        IJSRuntime jsRuntime,
        ICookieClientService cookiesServices): ITokenClientService
    {
        #region Fields

        private readonly string tokenKey = "_accessToken";

        #endregion Fields

        #region Public Methods

        public async Task SetToken(string accessToken)
        {
            try
            {
                await cookiesServices.SetCookies(tokenKey, accessToken, 1); // stores token in cookies
                await jsRuntime.InvokeVoidAsync("localStorage.setItem", tokenKey, accessToken); // stored in localStorage
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
                token = await cookiesServices.GetCookies(tokenKey);
            }
            catch (InvalidOperationException ex)
            {
                Console.Error.WriteLine($"[TokenClientService] Failed to get token from cookies: {ex.Message}");
            }

            if (string.IsNullOrEmpty(token))
            {
                try
                {
                    token = await jsRuntime.InvokeAsync<string>("localStorage.getItem", tokenKey);
                }
                catch (InvalidOperationException ex)
                {
                    Console.Error.WriteLine($"[TokenClientService] Failed to get token from localStorage: {ex.Message}");
                }
            }

            Console.WriteLine($"[TokenClientService] Loaded token: {(token == null ? "null" : token.Substring(0, Math.Min(10, token.Length)) + "...")}");
           
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
    