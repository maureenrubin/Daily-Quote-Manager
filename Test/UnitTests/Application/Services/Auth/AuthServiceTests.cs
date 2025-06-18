using DailyQuoteManager.Application.DTOs.Auth.Register;
using DailyQuoteManager.Application.Interfaces.Auth;
using DailyQuoteManager.Application.Services.Auth;
using DailyQuoteManager.Domain.Entities;
using DailyQuoteManager.Domain.Interfaces;
using DailyQuoteManager.Application.Common.Responses;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Tests.UnitTests.Application.Services.Auth
{
    public class AuthServiceTests
    {
        private readonly Mock<IUserRepository> _userRepo = new();
        private readonly Mock<IPasswordHasherService> _passwordHasher = new();
        private readonly Mock<IUnitOfWork> _unitOfWork = new();
        private readonly Mock<ITokenService> _tokenService = new();
        private readonly Mock<IRefreshTokenRepository> _refreshTokenRepo = new();
        private readonly Mock<ILogger<AuthService>> _logger = new();

        private readonly AuthService _service;

        public AuthServiceTests()
        {
            _service = new AuthService(
                _userRepo.Object,
                _passwordHasher.Object,
                _unitOfWork.Object,
                _tokenService.Object,
                _refreshTokenRepo.Object,
                _logger.Object
            );
        }

        [Fact]
        public async Task RegisterUserAsync_ShouldReturnFalse_WhenEmailAlreadyExists()
        {
            // Arrange
            var dto = new RegisterUserInputRequestDto { Email = "test@email.com" };
            _userRepo.Setup(x => x.GetByEmailAsync(dto.Email)).ReturnsAsync(new ApplicationUser());

            // Act
            var result = await _service.RegisterUserAsync(dto);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Email already exists.", result.Message);
        }

        [Fact]
        public async Task RegisterUserAsync_ShouldRegister_WhenEmailIsUnique()
        {
            // Arrange
            var dto = new RegisterUserInputRequestDto
            {
                FullName = "Test User",
                Email = "unique@email.com",
                Password = "SecurePassword123",
                Role = "Admin"
            };

            _userRepo.Setup(x => x.GetByEmailAsync(dto.Email)).ReturnsAsync((ApplicationUser?)null);
            _passwordHasher.Setup(x => x.HashPassword(dto.Password)).Returns("hashed-password");

            // Act
            var result = await _service.RegisterUserAsync(dto);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("Registration successful as Admin.", result.Message);
            _userRepo.Verify(x => x.AddAsync(It.IsAny<ApplicationUser>()), Times.Once);
            _unitOfWork.Verify(x => x.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task LoginAsync_ShouldReturnNull_WhenUserNotFound()
        {
            // Arrange
            var email = "notfound@email.com";
            var password = "Password";
            _userRepo.Setup(x => x.GetByEmailAsync(email)).ReturnsAsync((ApplicationUser?)null);

            // Act
            var result = await _service.LoginAsync(email, password);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task LoginAsync_ShouldReturnNull_WhenPasswordIncorrect()
        {
            // Arrange
            var email = "user@email.com";
            var password = "wrongpassword";

            var user = new ApplicationUser
            {
                AppUserId = Guid.NewGuid(),
                Email = email,
                PasswordHash = "correcthash"
            };

            _userRepo.Setup(x => x.GetByEmailAsync(email)).ReturnsAsync(user);
            _passwordHasher.Setup(x => x.VerifyPassword(password, user.PasswordHash)).Returns(false);

            // Act
            var result = await _service.LoginAsync(email, password);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task LoginAsync_ShouldReturnToken_WhenCredentialsValid()
        {
            // Arrange
            var email = "user@email.com";
            var password = "correctpassword";
            var userId = Guid.NewGuid();

            var user = new ApplicationUser
            {
                AppUserId = userId,
                Email = email,
                PasswordHash = "hashed"
            };

            // Use consistent fake refresh token string
            var refreshTokenValue = "refresh-token";
            var expectedToken = new TokenResponseDto("access-token", refreshTokenValue);

            _userRepo.Setup(x => x.GetByEmailAsync(email)).ReturnsAsync(user);
            _passwordHasher.Setup(x => x.VerifyPassword(password, user.PasswordHash)).Returns(true);
            _tokenService.Setup(x => x.GenerateToken(user)).Returns(expectedToken);
            _refreshTokenRepo.Setup(x => x.AddAsync(It.IsAny<RefreshToken>(), user.AppUserId))
                .ReturnsAsync(new RefreshToken { Token = refreshTokenValue });

            _unitOfWork.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

            // Act
            var result = await _service.LoginAsync(email, password);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedToken.AccessToken, result.AccessToken);
            Assert.Equal(refreshTokenValue, result.RefreshToken);
            _refreshTokenRepo.Verify(x => x.AddAsync(It.IsAny<RefreshToken>(), userId), Times.Once);
            _unitOfWork.Verify(x => x.SaveChangesAsync(), Times.Once);
        }
    }
}
