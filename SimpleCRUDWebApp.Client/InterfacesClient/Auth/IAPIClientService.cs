namespace DailyQuoteManager.Client.InterfacesClient.Auth
{
    public interface IAPIClientService
    {

        #region Public Methods

        Task<HttpResponseMessage> GetAsync(string endpoint);

        Task<HttpResponseMessage> PostDataAsync(string endpoint, object obj);

        #endregion Public Methods 
    }
}
