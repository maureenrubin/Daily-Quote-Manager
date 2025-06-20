 namespace DailyQuoteManager.Client.InterfacesClient.Auth
{
    public interface IAuthClientService
    {

        Task<bool> LoginAsync(string email, string password);
    }
}
