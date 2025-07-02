
using DailyQuoteManager.Domain.Entities;

namespace DailyQuoteManager.Domain.Interfaces
{
    public interface IRefreshTokenRepository
    {
        #region Public Methods 

        Task<RefreshTokens?> AddAsync(RefreshTokens refreshToken, Guid appUserId);

        Task<RefreshTokens?> GetByTokenAsync(string token);

        Task<bool> DisablerUserTokenAsync(string token);

        Task<bool> IsRefreshTokenValidAsync(string token);

        Task<IEnumerable<RefreshTokens>> GetByTokenListAsync(string token);

        #endregion Public Methods

    }
}
