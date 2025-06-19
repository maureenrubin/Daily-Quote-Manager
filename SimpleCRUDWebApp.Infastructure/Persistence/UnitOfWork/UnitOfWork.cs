using DailyQuoteManager.Domain.Interfaces;
using DailyQuoteManager.Infrastructure.Persistence.DatabaseContext;

namespace DailyQuoteManager.Infrastructure.Persistence.UnitOfWork
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
