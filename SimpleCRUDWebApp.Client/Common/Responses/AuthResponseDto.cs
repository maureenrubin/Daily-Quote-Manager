using System.Text.Json.Serialization;

namespace DailyQuoteManager.Client.Common.Responses
{
    public record AuthResponseDto
    {
        public string AccessToken { get; set; } = string.Empty;

        public string RefreshToken { get; set; }

        public string Email { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;

        public DateTime TokenExpired { get; set; } = DateTime.UtcNow;

        [JsonConstructor]
        public AuthResponseDto(string accessToken, string refreshToken, string email, string role, DateTime tokenExpired)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
            Email = email;
            Role = role;
            TokenExpired = tokenExpired;
        }


        public AuthResponseDto(string accessToken, string token, string email, string role)
           : this(accessToken, token, email, role, DateTime.UtcNow)
        {
        }
    }
}
