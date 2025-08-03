using DailyQuoteManager.Domain.Entities;
using DailyQuoteManager.Domain.Enums;

namespace DailyQuoteManager.Domain.Interfaces
{
    public interface IQuotesRepository
    {
        Task<Quotes?> GetQuotesByIdAsync(Guid id);

        Task<IEnumerable<Quotes>> GetAllPublicQuotesAsync();

        Task<IEnumerable<Quotes>> GetQuotesByCategoryAsync(QuoteCategory category);

        Task<IEnumerable<Quotes>> GetByUserIdAsync(Guid userId);

        Task<bool> ExistsAsync(Guid quoteId);

    }
}
