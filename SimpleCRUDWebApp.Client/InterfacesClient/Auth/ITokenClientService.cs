namespace DailyQuoteManager.Client.InterfacesClient.Auth
{
    public interface ITokenClientService
    {
        Task SetToken(string accessToken);

        Task<string> GetToken();

        Task RemoveToken();
    }
}
