using DailyQuoteManager.Application.Common.Responses;
using DailyQuoteManager.Application.Contracts.Interfaces.Auth;
using DailyQuoteManager.Application.DTOs.Auth.Login;
using DailyQuoteManager.Application.DTOs.Auth.Register;
using DailyQuoteManager.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DailyQuoteManager.Api.Controllers.Auth
{

    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(
                 IAuthService authService,
                 IRefreshTokenService refreshTokenService,
                 IRefreshTokenRepository refreshTokenRepository) : ControllerBase
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
                    Success = false,
                    ErrorMessage = "Validation failed.",
                    ValidationErrors = errors
                });
            }

            var (success, message) = await authService.RegisterUserAsync(request);

            if (!success)
            {
                return BadRequest(new Response
                {
                    Success = false,
                    ErrorMessage = message
                });
            }

            return Ok(new Response
            {
                Success = true,
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


        [HttpPost("refresh-token")]
        public async Task<ActionResult<TokenResponseDto>> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshtoken"];
           
            if (string.IsNullOrEmpty(refreshToken))
            {
                return BadRequest(new Response
                {
                    Success = false,
                    ErrorMessage = "RefreshToken is required."
                });
            }

            var refreshTokenEntity = await refreshTokenRepository.GetByTokenAsync(refreshToken);
           
            if(refreshTokenEntity == null)
            {
                return BadRequest(new Response
                {
                    Success = false,
                    ErrorMessage = "Failed to refresh the token. Token may be expired or invalid"
                });
            }
            var appUserId = refreshTokenEntity.AppUserId;

            var tokenResponse = await refreshTokenService.RefreshTokenAsync(refreshToken, appUserId);
            if (tokenResponse == null)
            {
                return BadRequest(new Response
                {
                    Success = false,
                    ErrorMessage = "Failed to refresh the token. Token may be expired or invalid."
                });
            }
            return Ok(new Response 
            { 
                Success = true,
                Message = "Token refreshed successfully",
                Data = tokenResponse
            });
        }


        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var refreshToken = Request.Cookies["refreshtoken"];
            var result = await authService.LogoutAsync(refreshToken);

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Message);
        }

    }
        #endregion Public Methods
 }
