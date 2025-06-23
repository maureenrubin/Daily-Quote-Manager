using DailyQuoteManager.Application.Contracts.Persistence;
using DailyQuoteManager.Domain.Entities;
using DailyQuoteManager.Domain.Interfaces;
using DailyQuoteManager.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace DailyQuoteManager.Persistence.Repositories
{
    public class RefreshTokenRepository(AppDbContext _dbContext,
        IRefreshTokenRepository refreshTokenRepository,
        IBaseRepository<RefreshToken> repository) : IRefreshTokenRepository
    {
        #region Public Methods 


        public async Task<RefreshToken?> AddAsync(RefreshToken refreshToken, Guid appUserId)
        {
            refreshToken.AppUserId = appUserId;

            await repository.AddAsync(refreshToken);
            return refreshToken;
        }

        public async Task<RefreshToken?> GetByTokenAsync(string token)
        {
            return await _dbContext.RefreshTokens
                                 .Include(r => r.User)
                                 .FirstOrDefaultAsync(r => r.Token == token);
        }
        public async Task <bool> DisablerUserTokenAsync(string token)
        {
            var refreshTokens = await _dbContext.RefreshTokens
                .Where(r => r.Token == token)
                .ToListAsync();

            if (refreshTokens.Count == 0)
                return false;

            foreach(var refreshToken in refreshTokens)
            {
                refreshToken.Enable = false;
                _dbContext.Update(refreshToken);
            }
            return true;
        }

        public async Task<bool> IsRefreshTokenValidAsync(string token)
        {
            var isValid = await _dbContext.RefreshTokens
                                .Where(r => r.Token == token && r.Enable && r.ExpiresAt >= DateTime.UtcNow.Date)
                                .AnyAsync();

            return isValid;
        }


        #endregion Public Methods

    }
}
