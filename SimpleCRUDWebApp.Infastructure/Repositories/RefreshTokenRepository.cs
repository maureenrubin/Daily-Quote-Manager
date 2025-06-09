
using DailyQuoteManager.Domain.Entities;
using DailyQuoteManager.Domain.Interfaces;
using DailyQuoteManager.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DailyQuoteManager.Infrastructure.Repositories
{
    public class RefreshTokenRepository(AppDbContext _dbContext) : IRefreshTokenRepository
    {
        #region Public Methods 


        public async Task<RefreshToken?> AddAsync(RefreshToken refreshToken, Guid appUserId)
        {
            refreshToken.UserId = appUserId;

            await _dbContext.RefreshTokens.AddAsync(refreshToken);
            await _dbContext.SaveChangesAsync();
            return refreshToken;
        }

        public async Task<RefreshToken?> GetByTokenAsync(string token)
        {
            return await _dbContext.RefreshTokens
                                 .Include(r => r.User)
                                 .FirstOrDefaultAsync(r => r.Token == token);
        }



        #endregion Public Methods

    }
}
