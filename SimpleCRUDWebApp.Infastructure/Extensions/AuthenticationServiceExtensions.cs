
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace DailyQuoteManager.Infrastructure.Extensions
{
   public static class AuthenticationServiceExtensions
    {
        #region Public Methods

        public static IServiceCollection AddJwtAuthentication(
            this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JWTSettings");

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                 .AddJwtBearer(options =>
                 {
                     options.TokenValidationParameters = new TokenValidationParameters
                     {
                         ValidateIssuerSigningKey = true,
                         IssuerSigningKey = new SymmetricSecurityKey(
                             Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]!)),
                         ValidateIssuer = true,
                         ValidateAudience = true,
                         ValidateLifetime = true,
                         ValidIssuer = jwtSettings["Issuer"],
                         ValidAudience = jwtSettings["Audience"],
                         ClockSkew = TimeSpan.Zero,
                         RoleClaimType = ClaimTypes.Role
                     };
                 });
            return services;

        }
            #endregion Public Methods

        }
}
