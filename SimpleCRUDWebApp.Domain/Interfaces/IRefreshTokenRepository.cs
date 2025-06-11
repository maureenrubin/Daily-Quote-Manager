
using DailyQuoteManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DailyQuoteManager.Domain.Interfaces
{
    public interface IRefreshTokenRepository
    {
        #region Public Methods 

        Task<RefreshToken?> AddAsync(RefreshToken refreshToken, Guid appUserId);

        Task<RefreshToken?> GetByTokenAsync(string token);

        #endregion Public Methods

    }
}
