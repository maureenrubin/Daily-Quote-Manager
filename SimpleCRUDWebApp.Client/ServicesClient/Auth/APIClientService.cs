using DailyQuoteManager.Client.InterfacesClient.Auth;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;

namespace DailyQuoteManager.Client.ServicesClient.Auth
{
    public class APIClientService (
        HttpClient httpClient,
        ITokenClientService tokenService,
        IAuthClientService authService) : IAPIClientService
    {

        #region Public Methods

        public async Task<HttpResponseMessage> GetAsync(string endpoint)
        {
            var token = await tokenService.GetToken();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", parameter: token);
            var responseMessage = await httpClient.GetAsync(endpoint);

            if(responseMessage.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                var refreshTokenResult = await authService.RefreshTokenAsync();

                if (!refreshTokenResult)
                {
                    await authService.LogoutAsync();
                }

                var newToken = await tokenService.GetToken();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", newToken);

                var newResponse = await httpClient.GetAsync(endpoint);
                return newResponse;
            }

            return responseMessage;
        }

        public async Task<HttpResponseMessage> PostDataAsync(string endpoint, object obj)
        {
            var token = await tokenService.GetToken();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await httpClient.PostAsJsonAsync(endpoint, obj);

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                var resfreshTokenResult = await authService.RefreshTokenAsync();
                if (!resfreshTokenResult)
                {
                    await authService.LogoutAsync();
                }

                var newToken = await tokenService.GetToken();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", newToken);
                var newResponse = await httpClient.PostAsJsonAsync(endpoint, obj);
                return newResponse;
            }

            return response;
        }

        #endregion Public Methods
    }
}
