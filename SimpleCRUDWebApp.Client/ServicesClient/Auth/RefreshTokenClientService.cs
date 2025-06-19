using DailyQuoteManager.Client.InterfacesClient.Auth;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace DailyQuoteManager.Client.ServicesClient.Auth
{
    public class RefreshTokenClientService(
        ProtectedLocalStorage protectedLocalStorage): IRefreshTokenClientService
    {
        #region Fields

        public readonly string key = "refresh_token";

        #endregion Fields

        #region Public Methods

        public async Task Set(string value)
        {
            await protectedLocalStorage.SetAsync(key, value);
        }

        public async Task<string> Get()
        {
            var result = await protectedLocalStorage.GetAsync<string>(key);
            if (result.Success)
                return result.Value!;

            return string.Empty;
        }

        public async Task Remove()
        {
            await protectedLocalStorage.DeleteAsync(key);
        }

        #endregion Public Methods


    }
}
