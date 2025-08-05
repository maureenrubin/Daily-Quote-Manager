using DailyQuoteManager.Domain.Enums;

namespace DailyQuoteManager.Application.DTOs.Quote.Quotes
{
    public sealed class QuotesInputReqDto
    {
        #region Properties

        public Guid? QuoteId { get; set; }

        public string Text { get; set; } = string.Empty;

        public string Author { get; set; } = "Unknown";

        public QuoteCategory Category { get; set; }

        public bool IsPublic { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Guid? AddedByUserId { get; set; }

        #endregion Propeties
    }
}
