using DailyQuoteManager.Application.Common.Responses;
using DailyQuoteManager.Domain.Entities;

namespace DailyQuoteManager.Application.Contracts.Interfaces.Auth
{
    public interface ITokenService
    {
        #region Public Methods

        string GenerateAccessToken(ApplicationUser user);

        RefreshTokens GenerateRefreshToken();

        TokenResponseDto GenerateToken(ApplicationUser user);
        #endregion Public Methods


    }
}
