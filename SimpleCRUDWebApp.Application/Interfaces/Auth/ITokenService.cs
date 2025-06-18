using DailyQuoteManager.Application.Common.Responses;
using DailyQuoteManager.Domain.Entities;

namespace DailyQuoteManager.Application.Interfaces.Auth
{
    public interface ITokenService
    {
        #region Public Methods

        string GenerateAccessToken(ApplicationUser user);

        RefreshToken GenerateRefreshToken();

        TokenResponseDto GenerateToken(ApplicationUser user);
        #endregion Public Methods


    }
}
