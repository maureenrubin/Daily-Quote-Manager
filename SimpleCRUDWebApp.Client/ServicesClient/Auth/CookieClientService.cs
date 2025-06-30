using DailyQuoteManager.Client.InterfacesClient.Auth;
using Microsoft.JSInterop;

namespace DailyQuoteManager.Client.ServicesClient.Auth
{
    #region Public Methods
    public class CookieClientService(IJSRuntime jSRuntime) : ICookieClientService
    {
        public async Task<string> GetCookies(string key)
        {
            try
            {
                return await jSRuntime.InvokeAsync<string>("getCookie", key);
            }
            catch (InvalidOperationException)
            {
                return null!;
            }
        }

        public async Task RemoveCookies(string key)
        {
            try
            {
                await jSRuntime.InvokeVoidAsync("deleteCookie", key);
            }
            catch (InvalidOperationException)
            {
            }
        }

        public async Task SetCookies(string key, string value, int days)
        {
            try
            {
                await jSRuntime.InvokeVoidAsync("setCookie", key, value, days);
            }
            catch (InvalidOperationException)
            {
            }
        }
    }

    #endregion Public Methods

}
