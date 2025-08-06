using DailyQuoteManager.Application.Common.Validators;
using DailyQuoteManager.Application.DTOs.Quote.Quotes;
using DailyQuoteManager.Client.DTOs.Quotes;

namespace DailyQuoteManager.Application.Contracts.Interfaces.Quote
{
    public interface IQuoteService
    {
        Task<ValidationResult<QuotesOutputDto>> CreateQuotesAsync(QuotesInputReqDto request);

        Task<ValidationResult<QuotesOutputDto>> UpdateQuotesAsync(Guid quotesId, QuotesInputReqDto request);

        Task<ValidationResult<QuotesInputReqDto>> DeleteQuotesAsync(Guid quotesId);
    }
}
