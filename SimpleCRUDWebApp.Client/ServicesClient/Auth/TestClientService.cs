namespace DailyQuoteManager.Client.ServicesClient.Auth
{
    public class TestClientService
    {
        private readonly HttpClient _httpClient;
        public TestClientService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
        }

        public async Task<string> Testing()
        {
            var response = await _httpClient.GetAsync("quotes/testingconnection");

            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsStringAsync();

            return "Connection Failed.";
        
        }
    }
}
