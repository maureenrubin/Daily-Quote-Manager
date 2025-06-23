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

            var newUser = new ApplicationUser
            {
                FullName = request.FullName,
                Email = request.Email,
                PasswordHash = hashedPassword,
                CreatedAt = DateTime.UtcNow,
                Role = roleToAssign
            };

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

            var refreshToken = new RefreshToken
            {
                RefreshTokenId = Guid.NewGuid(),
                Token = Guid.NewGuid().ToString(),
                ExpiresAt = DateTime.UtcNow.AddMonths(1),
                CreatedAt = DateTime.UtcNow,
                Enable = true,
                AppUserId = user.AppUserId,
                Email = user.Email
            };

            var savedRefreshToken = await refreshTokenRepository.AddAsync(refreshToken, user.AppUserId);
            await unitOfWork.SaveChangesAsync();

            logger.LogInformation("User logged in successfully.");

            if(savedRefreshToken == null)
            {
                logger.LogError("Refresh token creation failed.");
                return null!;
            }

            return new TokenResponseDto(token.AccessToken, savedRefreshToken.Token);
        }



        #endregion Public Methods


    }
}
