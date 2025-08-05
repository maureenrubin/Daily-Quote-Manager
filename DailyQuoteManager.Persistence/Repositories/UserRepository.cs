using DailyQuoteManager.Domain.Entities;
using DailyQuoteManager.Application.Contracts.Persistence;
using DailyQuoteManager.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace DailyQuoteManager.Persistence.Repositories
{
    public class UserRepository(AppDbContext _dbContext): IUserRepository 
    {

        #region Fields

        #endregion Fields

        #region Public Methods

        public async Task<ApplicationUser?> GetByEmailAsync(string email)
        {
            return await _dbContext.ApplicationUsers
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<ApplicationUser?> GetUserByIdAsync(Guid appUserId)
        {
            return await _dbContext.ApplicationUsers
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.AppUserId == appUserId);
        }

        public async Task AddAsync(ApplicationUser user)
        {
            var existingUser = await GetByEmailAsync(user.Email);
            if(existingUser != null)
            {
                throw new InvalidOperationException($"User with email '{user.Email}' already exists.");
            }
            // Add the user
            await _dbContext.ApplicationUsers.AddAsync(user);
            await _dbContext.SaveChangesAsync();

        }

        public async Task UpdateAsync(ApplicationUser user)
        {
            _dbContext.ApplicationUsers.Update(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid appUserId)
        {
            var user = await _dbContext.ApplicationUsers.FindAsync(appUserId);
            if (user != null)
            {
                _dbContext.ApplicationUsers.Remove(user);
                await _dbContext.SaveChangesAsync();
            }
        }

        #endregion Public Methods
    }
}
