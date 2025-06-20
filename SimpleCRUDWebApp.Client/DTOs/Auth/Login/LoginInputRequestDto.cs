namespace DailyQuoteManager.Client.DTOs.Auth.Login
{
    public class LoginInputRequestDto
    {
        #region Properties
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        #endregion Properties
    }
}
