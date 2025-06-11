using DailyQuoteManager.Application.DTOs.Auth.Register;
using DailyQuoteManager.Application.Services.Interfaces;
using DailyQuoteManager.Domain.Entities;
using DailyQuoteManager.Domain.Interfaces;
using Microsoft.Extensions.Logging; 

namespace DailyQuoteManager.Application.Services
{
    public class AuthService (IUserRepository userRepository,
                              IRefreshTokenRepository refreshTokenRepository,
                              IPasswordHasherService passwordHasherService,
                              IUnitOfWork unitOfWork,
                              ITokenService tokenService,
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


        #endregion Public Methods


    }
}
