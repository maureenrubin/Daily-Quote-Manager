using DailyQuoteManager.Client.InterfacesClient.Auth;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;

namespace DailyQuoteManager.Client.Security
{
    public class CustomAuthStateProvider
        (ITokenClientService tokenService,
         NavigationManager navigationManager,
         IAuthClientService authService) : AuthenticationStateProvider
    {
        #region Public Methods

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var token = await tokenService.GetToken();
                if (string.IsNullOrWhiteSpace(token))
                {
                    return MarkAsUnauthorize();
                }

                var readJWT = new JwtSecurityTokenHandler().ReadJwtToken(token);
                var identity = new ClaimsIdentity(readJWT.Claims, "JWT");
                var principals = new ClaimsPrincipal(identity);

                return await Task.FromResult(new AuthenticationState(principals));
            }
            catch
            {
                return MarkAsUnauthorize();
            }
        }

        public async Task NotifyUserAuthentication()
        {
            var token = await tokenService.GetToken();
            var identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");
            var user = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        public async Task Logout()
        {
            await authService.Logoutasync();
            navigationManager.NavigateTo("/login");
        }


        private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var claims = new List<Claim>();
            var payload = jwt.Split('.')[1];

            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

            foreach(var kvp in keyValuePairs!)
            { 

                if(kvp.Value is JsonElement element && element.ValueKind == JsonValueKind.Array)
                {
                    foreach (var item in element.EnumerateArray())
                    {
                        claims.Add(new Claim(kvp.Key, item.ToString()));
                    }
                }
                else
                {
                    claims.Add(new Claim(kvp.Key, kvp.Value.ToString()!));
                }
            }
            return claims;
        }

        private byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2:
                    base64 += "==";
                    break;

                case 3:
                    base64 += "=";
                    break;
            }
            return Convert.FromBase64String(base64);
        }

        private AuthenticationState MarkAsUnauthorize()
        {
            try
            {
                var state = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
                NotifyAuthenticationStateChanged(Task.FromResult(state));
                return state;
            }
            catch 
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }
        }


        #endregion Public Methods

    }
}
