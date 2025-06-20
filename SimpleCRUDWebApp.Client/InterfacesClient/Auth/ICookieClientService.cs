namespace DailyQuoteManager.Client.InterfacesClient.Auth
{
    public interface ICookieClientService
    {
        #region Public Methods

        Task<string> GetCookies(string key);

        Task RemoveCookies(string key);

        Task SetCookies(string key, string value, int days);

        #endregion Public Methods

    }
}
