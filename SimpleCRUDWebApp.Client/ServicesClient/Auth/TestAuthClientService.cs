using DailyQuoteManager.Client.InterfacesClient.Auth;

namespace DailyQuoteManager.Client.ServicesClient.Auth
{
    public class TestAuthClientService
        (APIClientService aPIService) : ITestAuthClientService
    {
        #region Public Methods

        public async Task<bool> Verify()
        {
            var response = await aPIService.GetAsync("testauth/verify");
            return response.IsSuccessStatusCode;
        }

        #endregion Public Methods

    }
}
