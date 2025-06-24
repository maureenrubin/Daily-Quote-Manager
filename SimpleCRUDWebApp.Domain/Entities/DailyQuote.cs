using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DailyQuoteManager.Domain.Entities
{
    public class DailyQuote
    {
        #region Properties

        [Key]
        public Guid DailyQuoteId { get; set; } = Guid.NewGuid();

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid QuoteId { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime DateShown { get; set; } = DateTime.UtcNow.Date;

        #endregion

        #region Navigation Properties

        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser ApplicationUser { get; set; } = null!;

        [ForeignKey(nameof(QuoteId))]
        public virtual Quotes Quote { get; set; } = null!;

        #endregion
    }
}
