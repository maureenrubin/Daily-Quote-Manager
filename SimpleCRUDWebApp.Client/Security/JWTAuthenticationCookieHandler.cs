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
                var token = Context.Request.Cookies["_accessToken"];
               
                if (string.IsNullOrEmpty(token))
                    return Task.FromResult(AuthenticateResult.Fail("Missing Token"));
               
                var handler = new JwtSecurityTokenHandler();
                var jwt = handler.ReadJwtToken(token);

                var identity = new ClaimsIdentity(jwt.Claims, Scheme.Name);
                var principals = new ClaimsPrincipal(identity);

                var ticket = new AuthenticationTicket(principals, Scheme.Name);
                return Task.FromResult(AuthenticateResult.Success(ticket));

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
