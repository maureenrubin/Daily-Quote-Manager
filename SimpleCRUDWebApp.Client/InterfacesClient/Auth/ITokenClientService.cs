namespace DailyQuoteManager.Client.InterfacesClient.Auth
{
    public interface ITokenClientService
    {
        Task<string> GetToken();

    }
}
