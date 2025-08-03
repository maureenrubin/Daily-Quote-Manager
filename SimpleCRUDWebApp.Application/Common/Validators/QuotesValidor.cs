using DailyQuoteManager.Application.DTOs.Quote.Quotes;
using DailyQuoteManager.Domain.Enums;

namespace DailyQuoteManager.Application.Common.Validators
{
    public class QuotesValidor
    {
        #region Public Methods

        public void QuotesCreate(QuotesInputReqDto request)
        {
            if (string.IsNullOrWhiteSpace(request.Text)) throw new ArgumentException("Quotes Text is required.", nameof(request.Text));

            if (string.IsNullOrWhiteSpace(request.Author)) throw new ArgumentException("Author is required", nameof(request.Author));

            if (request.AddedByUserId == null) throw new ArgumentException("User information is missing from the request.", nameof(request.AddedByUserId));

            if (!Enum.IsDefined(typeof(QuoteCategory), request.Category)) throw new ArgumentException("Invalid quote category.");

        }
        

        #endregion Public Methods

    }
}
