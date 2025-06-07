namespace DailyQuoteManager.Application.DTOs.Auth.RefreshToken
{
    public class RefreshTokenDto
    {
        #region Properties 

        public string Token { get; set; } = string.Empty;

        public DateTime? Expires { get; set; }

        public bool Enable { get; set; }

        public string Email { get; set; } = string.Empty;

        #endregion Properties

    }
}
