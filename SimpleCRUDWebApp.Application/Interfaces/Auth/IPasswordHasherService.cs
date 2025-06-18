namespace DailyQuoteManager.Application.Interfaces.Auth
{
    public interface IPasswordHasherService
    {
        #region Public Methods

        string HashPassword(string password);

        bool VerifyPassword(string password, string storedHash);

        #endregion Public Methods

    }
}
