using DailyQuoteManager.Client.Common.Responses;
using DailyQuoteManager.Client.DTOs.Auth.Register;

namespace DailyQuoteManager.Client.InterfacesClient.Auth
{
    public interface IAuthClientService
    {
        Task<bool> LoginAsync(string email, string password);

        Task LogoutAsync();

        Task<Response> RegisterAsync(RegisterUserInputRequestDto request);

        Task<bool> RefreshTokenAsync();

        
    }
}
