using DailyQuoteManager.Application.Common.Responses;
using DailyQuoteManager.Domain.Entities;
using System.Security.Claims;

namespace DailyQuoteManager.Application.Contracts.Interfaces.Auth
{
    public interface ITokenService
    {
        #region Public Methods

        string GenerateAccessToken(ApplicationUser user);

        RefreshTokens GenerateRefreshToken();

        TokenResponseDto GenerateToken(ApplicationUser user);

        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);

        #endregion Public Methods


    }
}
