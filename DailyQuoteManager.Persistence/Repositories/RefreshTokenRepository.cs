using DailyQuoteManager.Application.Contracts.Persistence;
using DailyQuoteManager.Domain.Entities;
using DailyQuoteManager.Domain.Interfaces;
using DailyQuoteManager.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace DailyQuoteManager.Persistence.Repositories
{
    public class RefreshTokenRepository(AppDbContext _dbContext,
        IBaseRepository<RefreshTokens> repository) : IRefreshTokenRepository
    {
        #region Public Methods 


        public async Task<RefreshTokens?> AddAsync(RefreshTokens refreshToken, Guid appUserId)
        {
            refreshToken.AppUserId = appUserId;

            await repository.AddAsync(refreshToken);
            return refreshToken;
        }

        public async Task<RefreshTokens?> GetByTokenAsync(string token)
        {
            return await _dbContext.RefreshTokens
                                 .Include(r => r.ApplicationUser)
                                 .FirstOrDefaultAsync(r => r.RefreshToken == token);
        }
        public async Task <bool> DisablerUserTokenAsync(string token)
        {
            var refreshTokens = await _dbContext.RefreshTokens
                .Where(r => r.RefreshToken == token)
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
                                .Where(r => r.RefreshToken == token && r.Enable && r.ExpiresAt >= DateTime.UtcNow.Date)
                                .AnyAsync();

            return isValid;
        }

        public async Task<IEnumerable<RefreshTokens>> GetByTokenListAsync(string token)
        {
            return await _dbContext.RefreshTokens
                .Where(r => r.RefreshToken == token)
                .ToListAsync();
        }

        #endregion Public Methods

    }
}
