using DailyQuoteManager.Application.Common.Responses;
using DailyQuoteManager.Application.DTOs.Auth.Register;

namespace DailyQuoteManager.Application.Contracts.Interfaces.Auth

{
    public interface IAuthService
    { 
        #region Public Methods

        Task<(bool Success, string Message)> RegisterUserAsync(RegisterUserInputRequestDto request);
       
        Task<TokenResponseDto> LoginAsync(string email, string password);


        #endregion Public Methods
    }
}
