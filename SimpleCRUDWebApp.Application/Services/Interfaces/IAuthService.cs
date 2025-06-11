using DailyQuoteManager.Application.DTOs.Auth.Register;

namespace DailyQuoteManager.Application.Services.Interfaces

{
    public interface IAuthService
    { 
        #region Public Methods

        Task<(bool Success, string Message)> RegisterUserAsync(RegisterUserInputRequestDto request);



        #endregion Public Methods
    }
}
