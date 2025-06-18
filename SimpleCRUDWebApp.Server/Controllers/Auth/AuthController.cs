using DailyQuoteManager.Application.Common.Responses;
using DailyQuoteManager.Application.DTOs.Auth.Login;
using DailyQuoteManager.Application.DTOs.Auth.Register;
using DailyQuoteManager.Application.Interfaces.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DailyQuoteManager.Api.Controllers.Auth
{

    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(
                 IAuthService authService) : ControllerBase
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

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginInputRequestDto request)
        {
            var tokenResponse = await authService.LoginAsync(request.Email, request.Password);

            if (tokenResponse == null)
                return Unauthorized(new { message = "Unauthorized" });

            return Ok(tokenResponse);
        }
    }
        #endregion Public Methods
 }
