using DailyQuoteManager.Client.InterfacesClient.Auth;
using Microsoft.JSInterop;

namespace DailyQuoteManager.Client.ServicesClient.Auth
{
    public class TokenClientService(
          ICookieClientService cookiesServices,
          IJSRuntime jsRuntime): ITokenClientService
    {
        #region Fields

        private readonly string tokenKey = "access_token";

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
            catch (InvalidOperationException ex) { }

            if (string.IsNullOrEmpty(token))
            {
                try
                {
                    token = await jsRuntime.InvokeAsync<string>("localStorage.getItem", tokenKey);
                }
                catch (InvalidOperationException ex) { }
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
    