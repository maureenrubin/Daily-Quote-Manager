namespace DailyQuoteManager.Application.Common.Responses
{
    public class AuthResponseDto
    {

        #region Properties

        public string AccessToken { get; set; } = string.Empty;

        public string Token { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;

        public DateTime TokenExpired { get; set; } = DateTime.UtcNow;

        #endregion Properties
    }
}
