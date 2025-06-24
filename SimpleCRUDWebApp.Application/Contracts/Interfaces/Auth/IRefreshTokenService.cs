using DailyQuoteManager.Application.Common.Responses;
using DailyQuoteManager.Domain.Entities;

namespace DailyQuoteManager.Application.Contracts.Interfaces.Auth
{
    public interface IRefreshTokenService
    {
       Task<TokenResponseDto?> RefreshTokenAsync(string refreshToken, Guid appUserId);

       Task<bool> DisableUserTokenAsync(string token);

       Task<RefreshToken?> GetByTokenAsync(string token);

       Task<RefreshToken?> CreateRefreshTokenAsync(ApplicationUser user, string token);
    }
}
