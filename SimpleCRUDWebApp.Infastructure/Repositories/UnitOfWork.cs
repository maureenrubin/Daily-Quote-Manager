using DailyQuoteManager.Domain.Interfaces;
using DailyQuoteManager.Infrastructure.Data;

namespace DailyQuoteManager.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Fields

        private readonly AppDbContext dbContext;

        #endregion Fields

        #region Properties

        public IUserRepository Users { get; }
        public IRefreshTokenRepository RefreshTokenRepository { get; }

        #endregion Properties



        #region Public Constructors

        public UnitOfWork
            (
            AppDbContext dbContext,
            IUserRepository userRepository,
            IRefreshTokenRepository refreshTokenRepository
            )
        {
            this.dbContext = dbContext;
            this.Users = userRepository;
            this.RefreshTokenRepository = refreshTokenRepository;
        }

        #endregion Public Constructors

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
