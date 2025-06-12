using DailyQuoteManager.Domain.Entities;

namespace DailyQuoteManager.Application.Services.Interfaces
{
    public interface ITokenService
    {
        #region Public Methods

        string GenerateAccessToken(ApplicationUser user);

        RefreshToken GenerateRefreshToken();

        //TokenResponseDto

        #endregion Public Methods


    }
}
