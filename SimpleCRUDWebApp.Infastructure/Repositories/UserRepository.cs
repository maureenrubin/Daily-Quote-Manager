using DailyQuoteManager.Domain.Entities;
using DailyQuoteManager.Domain.Interfaces;
using DailyQuoteManager.Infrastructure.Data;

namespace DailyQuoteManager.Infrastructure.Repositories
{
    public class UserRepository(AppDbContext _dbContext): IUserRepository 
    {

        #region Fields

        #endregion Fields

        #region Public Methods
        public async Task AddAsync(ApplicationUser user)
        {
            // Add the user
            await _dbContext.ApplicationUsers.AddAsync(user);
            await _dbContext.SaveChangesAsync();

        }

        #endregion Public Methods
    }
}
