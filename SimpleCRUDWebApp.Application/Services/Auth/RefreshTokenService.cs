using DailyQuoteManager.Application.Common.Responses;
using DailyQuoteManager.Application.Contracts.Interfaces.Auth;
using DailyQuoteManager.Application.DTOs.Auth.RefreshToken;
using DailyQuoteManager.Domain.Entities;
using DailyQuoteManager.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DailyQuoteManager.Application.Services.Auth
{
    public class RefreshTokenService
        ( ITokenService tokenService,
          IRefreshTokenRepository refreshTokenRepository,
          IUnitOfWork unitOfWork
        
        ): IRefreshTokenService
    {
        #region Public Methods

        public async Task<TokenResponseDto?> RefreshTokenAsync(string refreshToken, Guid appUserId)
        {
            // check if refreshtoken is valid
            var rtokenIsValid = await refreshTokenRepository.IsRefreshTokenValidAsync(refreshToken);
            if (!rtokenIsValid)
            {
                return null;
            }

            // get the refreshtoken entity
            var refreshTokenEntity = await GetByTokenAsync(refreshToken);
            if (refreshTokenEntity == null)
            {
                return null;
            }

            var token = tokenService.GenerateToken(refreshTokenEntity.ApplicationUser);

            // disable the old refreshtoken
            await DisableUserTokenAsync(refreshToken);

            var newRefreshToken = new RefreshToken
            {
                Token = token.RefreshToken,
                ExpiresAt = DateTime.UtcNow.AddMonths(1),
                AppUserId = refreshTokenEntity.AppUserId,
                Enable = true,
                Email = refreshTokenEntity.Email
            };

            var savedRefreshToken = await refreshTokenRepository.AddAsync(newRefreshToken, appUserId);

            if(savedRefreshToken == null)
            {
                return null;
            }

            await unitOfWork.SaveChangesAsync();

            var refreshTokenDto = new RefreshTokenDto
            {
                Token = savedRefreshToken.Token,
                Expires = savedRefreshToken.ExpiresAt,
                Enable = savedRefreshToken.Enable,
                Email = savedRefreshToken.Email
            };

            return new TokenResponseDto(
                token.AccessToken,
                newRefreshToken.Token);
        }

        public async Task<RefreshToken?> GetByTokenAsync(string token)
        {
            return await refreshTokenRepository.GetByTokenAsync(token);
        }


        public async Task<bool> DisableUserTokenAsync(string token)
        {
            var refreshTokens = await refreshTokenRepository.GetByTokenListAsync(token);
            if (!refreshTokens.Any())
                return false;

            foreach (var refreshToken in refreshTokens)
            {
                refreshToken.Enable = false;
            }

            return true;
        }

        public async Task<RefreshToken?> CreateRefreshTokenAsync(ApplicationUser user, string token)
        {
            var newRefreshToken = new RefreshToken
            {
                Token = token,
                ExpiresAt = DateTime.UtcNow.AddMonths(1),
                AppUserId = user.AppUserId,
                Enable = true,
                Email = user.Email!
            };

            await refreshTokenRepository.AddAsync(newRefreshToken, user.AppUserId);
            await unitOfWork.SaveChangesAsync();

            return newRefreshToken;

        }

        #endregion Public Methods
    }
}
