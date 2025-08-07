using DailyQuoteManager.Application.Contracts.Persistence;
using DailyQuoteManager.Persistence.DatabaseContext;

namespace DailyQuoteManager.Persistence.Repositories
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly AppDbContext dbContext;
        private readonly IUserRepository users;
        private readonly IRefreshTokenRepository refreshTokenRepository;
        private readonly IQuotesRepository quotesRepository;

        public UnitOfWork(AppDbContext dbContext,
            IUserRepository users,
            IRefreshTokenRepository refreshTokenRepository,
            IQuotesRepository quotesRepository)
        {
            this.dbContext = dbContext;
            this.users = users;
            this.refreshTokenRepository = refreshTokenRepository;
            this.quotesRepository = quotesRepository;
        }

      
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
