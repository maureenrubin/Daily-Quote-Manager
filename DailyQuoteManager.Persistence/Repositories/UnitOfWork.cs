using DailyQuoteManager.Application.Contracts.Persistence;
using DailyQuoteManager.Persistence.DatabaseContext;

namespace DailyQuoteManager.Persistence.Repositories
{
    public class UnitOfWork(AppDbContext dbContext,
                            IUserRepository Users,
                            IRefreshTokenRepository RefreshTokenRepository,
                            IQuotesRepository quotesRepository): IUnitOfWork
    {
        #region Properties
     
        public IUserRepository Users { get; } = Users;
        public IRefreshTokenRepository RefreshTokenRepository { get; } = RefreshTokenRepository;

        public IQuotesRepository quotesRepository { get; } = quotesRepository;

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
