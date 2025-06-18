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

        protected override async Task <AuthenticateResult> HandleAuthenticateAsync()
        {
            var token = Request.Cookies["access_token"];
            if (string.IsNullOrEmpty(token))
            {
                return AuthenticateResult.NoResult();
            }

            var readJWT = new JwtSecurityTokenHandler().ReadJwtToken(token);
            var identity = new ClaimsIdentity(readJWT.Claims, "JWT");
            var principals = new ClaimsPrincipal(identity);

            var ticket = new AuthenticationTicket(principals, Scheme.Name);
            return AuthenticateResult.Success(ticket);

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
