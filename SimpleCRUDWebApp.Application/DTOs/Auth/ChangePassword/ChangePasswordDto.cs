using System.ComponentModel.DataAnnotations;

namespace DailyQuoteManager.Application.DTOs.Auth.ChangePassword
{
    public sealed class ChangePasswordDto
    {
        #region Properties

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public string CurrentPassword { get; set; } = string.Empty;

        [Required]
        [MinLength(8)]
        public string NewPassword { get; set; } = string.Empty;

        [Required]
        [Compare("NewPassword")]
        public string ConfirmPassword { get; set; } = string.Empty;

        #endregion Properties

    }
}
