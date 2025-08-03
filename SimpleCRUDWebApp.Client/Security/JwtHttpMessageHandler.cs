
namespace DailyQuoteManager.Client.Security
{
    public class JwtHttpMessageHandler(IHttpContextAccessor httpContextAccessor) : DelegatingHandler
    {
     
        #region Protected Methods

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = httpContextAccessor.HttpContext?.Request.Cookies["access_token"];

            if (!string.IsNullOrWhiteSpace(token))
            {
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }

            return await base.SendAsync(request, cancellationToken);

        }
        #endregion Protected Methods

    }
}
