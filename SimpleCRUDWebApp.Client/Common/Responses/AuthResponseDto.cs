using Newtonsoft.Json;

public record AuthResponseDto
{

    public string AccessToken { get; init; }
    public string RefreshToken { get; init; }
    public string Email { get; init; }
    public string Role { get; init; }
    public DateTime TokenExpired { get; init; }


    [JsonConstructor]

    public AuthResponseDto(string accessToken, string refreshToken, string email, string role, DateTime tokenExpired)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
        Email = email;
        Role = role;
        TokenExpired = tokenExpired;
    }

    public AuthResponseDto(string accessToken, string refreshToken, string email, string role) 
        : this (accessToken, refreshToken, email, role, DateTime.UtcNow)
    {

    }
}