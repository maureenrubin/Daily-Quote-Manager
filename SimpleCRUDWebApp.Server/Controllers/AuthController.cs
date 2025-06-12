
using DailyQuoteManager.Application.Common.Responses;
using DailyQuoteManager.Application.DTOs.Auth.Register;
using DailyQuoteManager.Application.Services.Interfaces;
using DailyQuoteManager.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DailyQuoteManager.Api.Controllers
{

    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(
                 IAuthService authService,
                 IRefreshTokenService refreshTokenService,
                 IRefreshTokenRepository refreshTokenRepository,
                 IUnitOfWork unitOfWork) : ControllerBase
    {
        #region Public Methods 
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserInputRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(e => e.Value!.Errors.Any())
                    .ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value!.Errors.Select(e => e.ErrorMessage).ToArray()
                    );

                return BadRequest(new Response
                {
                    IsSuccess = false,
                    ErrorMessage = "Validation failed.",
                    ValidationErrors = errors
                });
            }

            var (success, message) = await authService.RegisterUserAsync(request);

            if (!success)
            {
                return BadRequest(new Response
                {
                    IsSuccess = false,
                    ErrorMessage = message
                });
            }

            return Ok(new Response
            {
                IsSuccess = true,
                Message = message
            });
        }

        #endregion Public Methods
    }
}
