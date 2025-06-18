using DailyQuoteManager.Client.InterfacesClient.Auth;
using Microsoft.JSInterop;

namespace DailyQuoteManager.Client.ServicesClient.Auth
{
    public class TokenClientService(
        IJSRuntime jsRuntime): ITokenClientService
    {
        #region Fields

        private readonly string tokenKey = "access_token";

        #endregion Fields

        #region Public Methods

        public async Task<string> GetToken()
        {
            var token = await jsRuntime.InvokeAsync<string>("localStorage.getItem");

            return token ?? string.Empty;
        }
       
            

        #endregion Public Methods


    }
}
    