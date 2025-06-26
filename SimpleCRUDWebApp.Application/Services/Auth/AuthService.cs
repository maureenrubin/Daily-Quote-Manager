using AutoMapper;
using DailyQuoteManager.Application.Common.Mapping.Profiles;
using DailyQuoteManager.Application.Common.Responses;
using DailyQuoteManager.Application.Contracts.Interfaces.Auth;
using DailyQuoteManager.Application.DTOs.Auth.Register;
using DailyQuoteManager.Domain.Entities;
using DailyQuoteManager.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace DailyQuoteManager.Application.Services.Auth
{  
    public class AuthService(IUserRepository userRepository,
                              IPasswordHasherService passwordHasherService,
                              IUnitOfWork unitOfWork,
                              ITokenService tokenService,
                              IRefreshTokenRepository refreshTokenRepository,
                              IRefreshTokenService refreshTokenService,
                              IMapper mapper,
                              ILogger<AuthService> logger) : IAuthService
    {

        #region Public Methods

        public async Task<(bool Success, string Message)> RegisterUserAsync(RegisterUserInputRequestDto request)
        {
            var existingUser = await userRepository.GetByEmailAsync(request.Email);
            if (existingUser != null)
            {
                logger.LogWarning("Registration Failed: Email already exists.");
                return (false, "Email already exists.");
            }

            var hashedPassword = passwordHasherService.HashPassword(request.Password);

            var roleToAssign = string.IsNullOrWhiteSpace(request.Role) ? "User" : request.Role;

            var newUser = mapper.Map<ApplicationUser>(request);
            newUser.PasswordHash = hashedPassword;
            newUser.CreatedAt = DateTime.UtcNow;
            newUser.Role = roleToAssign;


            await userRepository.AddAsync(newUser);
            await unitOfWork.SaveChangesAsync();

            logger.LogInformation("User registered successfully with role: {Role}", roleToAssign);

            return (true, $"Registration successful as {roleToAssign}.");
        }

        public async Task<TokenResponseDto> LoginAsync(string email, string password)
        {
            var user = await userRepository.GetByEmailAsync(email);
            if(user == null)
            {
                logger.LogWarning("Login failed: User not found.");
                return null!;
            }

            if(!passwordHasherService.VerifyPassword(password, user.PasswordHash))
            {
                logger.LogWarning("Login failed: Incorrect password.");
                return null!;
            }

            var token = tokenService.GenerateToken(user);

            var newRefreshToken = await refreshTokenService.CreateRefreshTokenAsync(user, token.RefreshToken);

            if(newRefreshToken == null)
            {
                logger.LogError("Failed to create refresh token during login.");
                return null!;
            }

            await unitOfWork.SaveChangesAsync();

            logger.LogInformation("User logged in successfully.");

            return new TokenResponseDto(token.AccessToken, newRefreshToken.Token);

        }

        public async Task<Response> LogoutAsync (string? refreshToken)
        {
            if (string.IsNullOrEmpty(refreshToken))
                return new Response
                {
                    IsSuccess = false,
                    ErrorMessage = "No refresh token provided."
                };

            var tokenEntity = await refreshTokenRepository.GetByTokenAsync(refreshToken);
            if(tokenEntity == null)
            {
                return new Response
                {
                    IsSuccess = false,
                    ErrorMessage = "Token not found or already disabled."
                };
            }

            await unitOfWork.SaveChangesAsync();
            return new Response
            {
                IsSuccess = true,
                Message = "Token disabled successfully."
            };
        }

        #endregion Public Methods


    }
}
