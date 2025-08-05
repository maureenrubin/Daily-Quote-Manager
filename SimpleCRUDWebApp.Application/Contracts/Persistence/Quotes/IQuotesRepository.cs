using DailyQuoteManager.Domain.Entities;
using DailyQuoteManager.Domain.Enums;

namespace DailyQuoteManager.Application.Contracts.Persistence
{
    public interface IQuotesRepository : IBaseRepository<Quotes>

    {
        Task<Quotes?> GetQuotesByIdAsync(Guid id);

        Task<IEnumerable<Quotes>> GetAllPublicQuotesAsync();

        Task<IEnumerable<Quotes>> GetQuotesByCategoryAsync(QuoteCategory category);

        Task<IEnumerable<Quotes>> GetByUserIdAsync(Guid userId);

        Task<bool> ExistsAsync(Guid quoteId);

    }
}
