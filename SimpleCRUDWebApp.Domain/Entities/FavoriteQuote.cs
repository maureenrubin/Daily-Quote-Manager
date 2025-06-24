using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DailyQuoteManager.Domain.Entities
{
    public class FavoriteQuote
    {
        #region Properties

        [Key]
        public Guid FavoriteQuoteId { get; set; } = Guid.NewGuid();

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid QuoteId { get; set; }

        [Required]
        public DateTime AddedAt { get; set; } = DateTime.UtcNow;

        #endregion

        #region Navigation Properties

        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser ApplicationUser { get; set; } = null!;

        [ForeignKey(nameof(QuoteId))]
        public virtual Quotes Quote { get; set; } = null!;

        #endregion
    }
}
