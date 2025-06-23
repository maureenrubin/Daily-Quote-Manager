using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Text.Encodings.Web;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace DailyQuoteManager.Client.Security
{
    public class JWTAuthenticationCookieHandler : AuthenticationHandler<CustomOption>
    {
        #region Public Constructors
        public JWTAuthenticationCookieHandler(
        IOptionsMonitor<CustomOption> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        TimeProvider time) : base(options, logger, encoder)
        {

        }

        #endregion Public Constructors


        #region Protected Methods

        protected override Task <AuthenticateResult> HandleAuthenticateAsync()
        {
            try
            {
                var token = Request.Cookies["access_token"];
                if (string.IsNullOrEmpty(token))
                {
                    return Task.FromResult(AuthenticateResult.NoResult());
                }

                var readJWT = new JwtSecurityTokenHandler().ReadJwtToken(token);
                var identity = new ClaimsIdentity(readJWT.Claims, "JWT");
                var principals = new ClaimsPrincipal(identity);

                var ticket = new AuthenticationTicket(principals, Scheme.Name);
                return Task.FromResult(AuthenticateResult.Success(ticket));

            }
            catch (Exception ex)
            {
                Logger.LogWarning(ex, "JWT validation failed");
                return Task.FromResult(AuthenticateResult.Fail("Invalid token."));
            }
          
        }

        protected override Task HandleChallengeAsync (AuthenticationProperties properties)
        {
            Response.Redirect("/");
            return Task.CompletedTask;
        }

        protected override Task HandleForbiddenAsync (AuthenticationProperties propeties)
        {
            Response.Redirect("/notfound404");
            return Task.CompletedTask;
        }

        #endregion Protected Methods

    }


    public class CustomOption : AuthenticationSchemeOptions
    {

    }
}
