using DailyQuoteManager.Client.Common.Responses;
using DailyQuoteManager.Client.InterfacesClient.Auth;
using Newtonsoft.Json;


namespace DailyQuoteManager.Client.ServicesClient.Auth
{
    public class AuthClientService : IAuthClientService
    {
        #region Fields 

        private readonly ITokenClientService _tokenService;
        private readonly IRefreshTokenClientService _refreshTokenService;
        private readonly HttpClient _httpClient;
        private readonly ILogger<AuthClientService> _logger;

        #endregion Fields

        #region Public Constructors

        public AuthClientService(
            ITokenClientService tokenService,
            IRefreshTokenClientService refreshTokenService,
            IHttpClientFactory httpClientFactory,
            ILogger<AuthClientService> logger)
        {
            _tokenService = tokenService;
            _refreshTokenService = refreshTokenService;
            _httpClient = httpClientFactory.CreateClient("ApiClient");
            _logger = logger;

        }

        #endregion Public Constructors


        #region Public Methods

        public async Task<bool> LoginAsync(string email, string password)
        {
            try
            {
                // Sends login request to the API
                var response = await _httpClient.PostAsJsonAsync("auth/login", new { email, password });

                if (response.IsSuccessStatusCode)
                {
                    // Reads the response body as JSON
                    var json = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<AuthResponseDto>(json);

                    if (result != null && !string.IsNullOrEmpty(result.AccessToken))
                    {
                        await _tokenService.SetToken(result.AccessToken);
                        await _refreshTokenService.Set(result.RefreshToken);
                        return true;
                    }
                    else
                    {
                        _logger.LogError("Token deserialization failed or token was empty.");
                    }
                }
                else
                {
                    _logger.LogError($"Login failed with status code: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during login.");
            }

            return false;
        }


        #endregion Public Methods

    }
}
