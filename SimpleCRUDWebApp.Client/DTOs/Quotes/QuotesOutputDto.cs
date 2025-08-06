using DailyQuoteManager.Domain.Enums;

namespace DailyQuoteManager.Client.DTOs.Quotes
{
    public class QuotesOutputDto
    {

        public Guid? QuoteId { get; set; }

        public string Text { get; set; } = string.Empty;

        public string Author { get; set; } = string.Empty;

        public QuoteCategory Category { get; set; }

        public bool IsPublic { get; set; } 

        public Guid? AddedByUserId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
