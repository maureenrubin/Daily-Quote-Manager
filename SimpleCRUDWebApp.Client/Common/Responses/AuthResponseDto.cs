using Newtonsoft.Json;
using System.Net.Http.Headers;

public record AuthResponseDto
{
    [JsonProperty("accessToken")]
    public string AccessToken { get; set; } = string.Empty;

    [JsonProperty("refreshToken")]
    public string RefreshToken { get; set; } = string.Empty;

    [JsonProperty("email")]
    public string Email { get; set; } = string.Empty;

    [JsonProperty("role")]
    public string Role { get; set; } = string.Empty;

    [JsonProperty("tokenExpired")]
    public DateTime TokenExpired { get; set; } = DateTime.UtcNow;
}
