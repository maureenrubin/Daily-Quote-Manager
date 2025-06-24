using DailyQuoteManager.Client.Common.Responses;
using DailyQuoteManager.Client.DTOs.Auth.Register;
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
        public async Task<Response> RegisterAsync(RegisterUserInputRequestDto request)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("auth/register", request);

                var responseContent = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {

                    var errorDto = JsonConvert.DeserializeObject<Response>(responseContent);
                    var errorMessage = errorDto?.ErrorMessage ?? "Failed to register.";

                    _logger.LogWarning("Registration failed: {ErrorMessage}", errorMessage);

                    return new Response
                    {
                        IsSuccess = false,
                        ErrorMessage = errorMessage
                    };
                
                }

                var registrationResponse = JsonConvert.DeserializeObject<Response>(responseContent);
                return registrationResponse ?? new Response { IsSuccess = true, Message = "Registration Successful" };

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occured during registration");
                return new Response { IsSuccess = false, ErrorMessage = "An error occured during registration." };
            }

        }


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


        public async Task Logoutasync()
        {
            var refreshToken = await _refreshTokenService.Get();
            _httpClient.DefaultRequestHeaders.Add("Cookie", $"_refreshToken={refreshToken}");
            var response = await _httpClient.PostAsync("auth/logout", null);
        }


        public async Task<bool> RefreshTokenAsync()
        {
            var refreshToken = await _refreshTokenService.Get();
            _httpClient.DefaultRequestHeaders.Add("Cookie", $"_refreshToken={refreshToken}");
            
            var response = await _httpClient.PostAsync("auth/logout", null);

            if (response.IsSuccessStatusCode)
            {
                var token = await response.Content.ReadAsStringAsync();
                if (!string.IsNullOrEmpty(token))
                {
                    var result = JsonConvert.DeserializeObject<AuthResponseDto>(token);
                    await _tokenService.SetToken(result!.AccessToken);
                    await _refreshTokenService.Set(result.RefreshToken);
                    return true;
                }
            }

            return false;
        }

        #endregion Public Methods

    }
}
