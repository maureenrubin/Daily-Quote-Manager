using DailyQuoteManager.Domain.Interfaces;
using DailyQuoteManager.Persistence.DatabaseContext;

namespace DailyQuoteManager.Persistence.Repositories
{
    public class UnitOfWork(AppDbContext dbContext,
                            IUserRepository Users,
                            IRefreshTokenRepository RefreshTokenRepository): IUnitOfWork
    {
        #region Properties

        public IUserRepository Users { get; } = Users;
        public IRefreshTokenRepository RefreshTokenRepository { get; } = RefreshTokenRepository;

        #endregion Properties


        #region Public Methods

        public async Task<int> SaveChangesAsync()
        {
            return await dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }

        #endregion Public Methods
    }
}
