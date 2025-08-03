using DailyQuoteManager.Domain.Entities;
using DailyQuoteManager.Domain.Enums;
using DailyQuoteManager.Domain.Interfaces;
using DailyQuoteManager.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace DailyQuoteManager.Persistence.Repositories
{
    public class QuotesRepository : BaseRepository<Quotes>, IQuotesRepository
    {
        #region Public Constructor

        public QuotesRepository(AppDbContext dbContext) : base(dbContext) { }

        #endregion Public Constructor

        #region Public Methods

        public async Task<Quotes?> GetQuotesByIdAsync(Guid id)
        {
            return await dbContext.Quotes
                .Include(q => q.ApplicationUser)
                .FirstOrDefaultAsync(q => q.QuoteId == id);
        } 

        public async Task<IEnumerable<Quotes>> GetAllPublicQuotesAsync()
        {
            return await dbContext.Quotes
                .Where(q => q.IsPublic)
                .OrderByDescending(q => q.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Quotes>> GetQuotesByCategoryAsync(QuoteCategory category)
        {
            return await dbContext.Quotes
                .Where(q => q.Category == category)
                .OrderByDescending(q => q.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Quotes>> GetByUserIdAsync(Guid userId)
        {
            return await dbContext.Quotes
                .Where(q => q.AddedByUserId == userId)
                .OrderByDescending(q => q.CreatedAt)
                .ToListAsync();
        }

        public async Task<bool> ExistsAsync(Guid quoteId)
        {
            return await dbContext.Quotes.AnyAsync(q => q.QuoteId == quoteId);
        }


        #endregion Public Methods

    }
}
