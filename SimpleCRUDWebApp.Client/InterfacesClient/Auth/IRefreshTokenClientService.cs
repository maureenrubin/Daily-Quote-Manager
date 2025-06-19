namespace DailyQuoteManager.Client.InterfacesClient.Auth
{
    public interface IRefreshTokenClientService
    {
        Task Set(string value);

        Task<string> Get();

        Task Remove();

    }
}
