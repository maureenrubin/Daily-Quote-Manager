using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace DailyQuoteManager.Client.Common.Events
{
    public class CookieEvents : CookieAuthenticationEvents
    {
        #region Public Methods

        public override Task RedirectToLogin(RedirectContext<CookieAuthenticationOptions> context)
        {
            context.RedirectUri = "/login";
            return base.RedirectToLogin(context);
        }


        #endregion Public Methods
    }
}
