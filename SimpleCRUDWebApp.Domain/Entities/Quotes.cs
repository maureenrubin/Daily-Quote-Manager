using DailyQuoteManager.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DailyQuoteManager.Domain.Entities
{
    public class Quotes
    {
        #region Properties

        [Key]
        public Guid QuoteId { get; set; } = Guid.NewGuid();

      
        public string Text { get; set; } = string.Empty;
        
        public string Author { get; set; } = "Unknown";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public QuoteCategory Category { get; set; } = QuoteCategory.General;

        public bool IsPublic { get; set; } = true;

        public Guid? AddedByUserId { get; set; }

        #endregion Properties


        #region Navigation Properties

        [ForeignKey(nameof(AddedByUserId))]
        public virtual ApplicationUser? ApplicationUser { get; set; }

        public virtual ICollection<FavoriteQuote> FavoritedByUsers { get; set; } = new List<FavoriteQuote>();

        public virtual ICollection<DailyQuote> DailyQuotes { get; set; } = new List<DailyQuote>();

        #endregion Navigation Properties

    }
}
