namespace DailyQuoteManager.Application.DTOs.Auth.Login
{
    public sealed class LoginInputRequestDto
    {

        #region Properties

        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        #endregion Properties
    }
}
