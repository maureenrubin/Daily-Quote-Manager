using DailyQuoteManager.Client.InterfacesClient.Auth;
using System.Net.Http.Headers;
namespace DailyQuoteManager.Client.Security
{
    public class JwtHttpMessageHandler : DelegatingHandler
    {
        #region Fields
        private readonly ITokenClientService _tokenService;
        #endregion Fields

        #region Public Constructors
        public JwtHttpMessageHandler(ITokenClientService tokenService)
        {
            _tokenService = tokenService;
        }
        #endregion Public Constructors

        #region Protected Methods

        
        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var token = await _tokenService.GetToken();

            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return await base.SendAsync(request, cancellationToken);

        }
        #endregion Protected Methods

    }
}
