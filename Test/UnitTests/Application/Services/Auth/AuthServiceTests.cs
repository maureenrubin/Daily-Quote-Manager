using AutoMapper;
using DailyQuoteManager.Application.Contracts.Interfaces.Auth;
using DailyQuoteManager.Application.Contracts.Persistence;
using DailyQuoteManager.Application.DTOs.Auth.Register;
using DailyQuoteManager.Application.Services.Auth;
using DailyQuoteManager.Domain.Entities;
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
        private readonly Mock<IRefreshTokenService> _refreshTokenService = new();
        private readonly Mock<IMapper> _mapper = new();
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
                _refreshTokenService.Object,
                _mapper.Object,
                _logger.Object
            );
        }

        [Fact]
        public async Task RegisterUserAsync_ShouldSucceed_WhenEmailIsNotTaken()
        {
            // Arrange
            var request = new RegisterUserInputRequestDto
            {
                FullName = "John Doe",
                Email = "john@example.com",
                Password = "Test1234!",
                Role = "" 
            };

            var mappedUser = new ApplicationUser
            {
                FullName = request.FullName,
                Email = request.Email
            };

            _userRepo.Setup(r => r.GetByEmailAsync(request.Email)).ReturnsAsync((ApplicationUser?)null);
            _passwordHasher.Setup(p => p.HashPassword(request.Password)).Returns("hashed-password");
            _mapper.Setup(m => m.Map<ApplicationUser>(request)).Returns(mappedUser);
            _userRepo.Setup(r => r.AddAsync(mappedUser)).Returns(Task.CompletedTask);
            _unitOfWork.Setup(u => u.SaveChangesAsync()).ReturnsAsync(1);

            // Act
            var result = await _service.RegisterUserAsync(request);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("Registration successful as DefaultUser.", result.Message);
            Assert.Equal("hashed-password", mappedUser.PasswordHash);
            Assert.Equal("DefaultUser", mappedUser.Role);

            _userRepo.Verify(r => r.AddAsync(It.IsAny<ApplicationUser>()), Times.Once);
            _unitOfWork.Verify(u => u.SaveChangesAsync(), Times.Once);
        }
    }
}
