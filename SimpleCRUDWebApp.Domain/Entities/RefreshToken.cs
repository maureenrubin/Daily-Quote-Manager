using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DailyQuoteManager.Domain.Entities
{
    public class RefreshToken
    {
        #region Properties

        [Key]
        public Guid RefreshTokenId { get; set; } = Guid.NewGuid();

        [ForeignKey("AppUserId")]
        public Guid AppUserId { get; set; }

        public string Token { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        public DateTime? Expires { get; set; } = DateTime.UtcNow;

        public bool Enabled { get; set; }

        [JsonIgnore]
        public virtual ApplicationUser ApplicationUser { get; set; } = default!;

        #endregion Properties
    }
}
