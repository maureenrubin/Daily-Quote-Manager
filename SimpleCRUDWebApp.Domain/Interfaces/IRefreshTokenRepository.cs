
using DailyQuoteManager.Domain.Entities;

namespace DailyQuoteManager.Domain.Interfaces
{
    public interface IRefreshTokenRepository
    {
        #region Public Methods 

        Task<RefreshToken?> AddAsync(RefreshToken refreshToken, Guid appUserId);

        Task<RefreshToken?> GetByTokenAsync(string token);

        Task<bool> DisablerUserTokenAsync(string token);

        Task<bool> IsRefreshTokenValidAsync(string token);


        #endregion Public Methods

    }
}
