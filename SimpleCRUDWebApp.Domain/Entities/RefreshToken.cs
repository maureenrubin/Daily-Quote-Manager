using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DailyQuoteManager.Domain.Entities
{
    public class RefreshToken
    {
        #region Properties

        [Key]
        public Guid TokenId { get; set; } = Guid.NewGuid();

        [Required]
        public string Token { get; set; } = string.Empty;

        public DateTime ExpiresAt { get; set; }

        public bool IsRevoked { get; set; } = false;

        [Required]
        public Guid UserId { get; set; }

        #endregion

        #region Navigation Properties

        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser User { get; set; } = null!;

        #endregion
    }
}
