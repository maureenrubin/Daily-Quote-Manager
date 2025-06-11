using DailyQuoteManager.Application.Services.Interfaces;

namespace DailyQuoteManager.Application.Services
{
    public class PasswordHasherService : IPasswordHasherService
    {

        #region Public Methods

        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string password, string storedHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, storedHash);
        }

        #endregion Public Methods

    }
}
