namespace DailyQuoteManager.Application.Services.Interfaces
{
    public interface IPasswordHasherService
    {
        #region Public Methods

        string HashPassword(string password);

        bool VerifyPassword(string password, string storedHash);

        #endregion Public Methods

    }
}
