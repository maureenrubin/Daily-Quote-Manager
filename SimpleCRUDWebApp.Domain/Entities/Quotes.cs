using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DailyQuoteManager.Domain.Entities
{
    public class Quotes
    {
        #region Properties

        [Key]
        public Guid QuoteId { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(1000)]
        public string Text { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string Author { get; set; } = "Unknown";

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [MaxLength(100)]
        public string Category { get; set; } = "General";

        public bool IsPublic { get; set; } = true;

        public Guid? AddedByUserId { get; set; }

        #endregion Properties


        #region Navigation Properties

        [ForeignKey(nameof(AddedByUserId))]
        public virtual ApplicationUser? AddedByUser { get; set; }

        public virtual ICollection<FavoriteQuote> FavoritedByUsers { get; set; } = new List<FavoriteQuote>();

        public virtual ICollection<DailyQuote> DailyQuotes { get; set; } = new List<DailyQuote>();

        #endregion Navigation Properties

    }
}
