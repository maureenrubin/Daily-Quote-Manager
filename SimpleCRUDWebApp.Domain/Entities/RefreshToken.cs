using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DailyQuoteManager.Domain.Entities
{
    public class RefreshToken
    {
        #region Properties

        [Key]
        public Guid RefreshTokenId { get; set; } = Guid.NewGuid();

        public string Token { get; set; } = string.Empty;

        public DateTime ExpiresAt { get; set; }

        public bool Enable { get; set; }

        public DateTime CreatedAt { get; set; }

        [Required]
        public string Email { get; set; } = string.Empty;


        [Required]
        public Guid AppUserId { get; set; }

        #endregion

        #region Navigation Properties

        [ForeignKey(nameof(AppUserId))]
        public virtual ApplicationUser ApplicationUser { get; set; } = null!;

        #endregion
    }
}
