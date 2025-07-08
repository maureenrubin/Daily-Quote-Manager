namespace DailyQuoteManager.Client.InterfacesClient.Auth
{
    public interface ITestAuthClientService
    {
        Task<bool> Verify();

    }
}
