
using DailyQuoteManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DailyQuoteManager.Domain.Interfaces
{
    public interface IRefreshTokenRepository
    {
        #region Public Methods 

        public async Task<RefreshToken?> AddAsync(RefreshToken refreshToken, Guid appUserId)
        {
            refreshToken.UserId = appUserId;

            await dbContext.RefreshTokens.AddAsync(refreshToken);
            await dbContext.SaveChangesAsync();
            return refreshToken;
        }

        #endregion Public Methods

    }
}
