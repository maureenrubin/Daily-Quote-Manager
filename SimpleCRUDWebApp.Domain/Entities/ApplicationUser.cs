using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DailyQuoteManager.Domain.Entities
{
    public class ApplicationUser
    {
        #region Properties

        [Key]
        public Guid AppUserId { get; set; }

        [Required]
        public string FullName { get; set; } = string.Empty;

        [Required, EmailAddress, MaxLength(250)]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        public string Gender { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        public string Role { get; set; } = string.Empty;

        [JsonIgnore]
        public virtual ICollection<Quotes> AddedQuotes { get; set; } = new List<Quotes>();

        [JsonIgnore]
        public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
        #endregion Propertoies
    }
}
