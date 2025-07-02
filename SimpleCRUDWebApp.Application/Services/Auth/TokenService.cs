using DailyQuoteManager.Application.Common.Responses;
using DailyQuoteManager.Application.Contracts.Interfaces.Auth;
using DailyQuoteManager.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;


namespace DailyQuoteManager.Application.Services.Auth
{
    public class TokenService(IConfiguration configuration) : ITokenService
    {
        #region Public Methods

        public string GenerateAccessToken (ApplicationUser user)
        {
            string secretKey = configuration["JWTSettings:SecretKey"]!;
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.AppUserId.ToString()),
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };


            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(3),
                SigningCredentials = creds,
                Issuer = configuration["JWTSettings:Issuer"],
                Audience = configuration["JWTSettings:Audience"]
            };

            return new JsonWebTokenHandler().CreateToken(tokenDescriptor);
        }

        public RefreshTokens GenerateRefreshToken()
        {
            var refreshToken = new RefreshTokens
            {
                RefreshTokenId = Guid.NewGuid(),
                RefreshToken = Guid.NewGuid().ToString(),
                ExpiresAt = DateTime.UtcNow.AddMonths(1),
                CreatedAt = DateTime.UtcNow,
                Enable = true,
            };

            return refreshToken;
        }

        public TokenResponseDto GenerateToken(ApplicationUser user)
        {
            var accessToken = GenerateAccessToken(user);
            var refreshToken = GenerateRefreshToken();

            return new TokenResponseDto(accessToken, refreshToken.RefreshToken);
        }

        #endregion Public Methods


    }


}
