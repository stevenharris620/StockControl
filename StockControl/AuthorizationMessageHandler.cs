using Blazored.LocalStorage;
using System.Net.Http.Headers;

namespace StockControl
{
    public class AuthorizationMessageHandler : DelegatingHandler
    {
        // adds token (if present) to request header - will do this automatically for each request

        private readonly ILocalStorageService _localStorage;

        public AuthorizationMessageHandler(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (await _localStorage.ContainKeyAsync("access_token"))
            {
                var token = await _localStorage.GetItemAsStringAsync("access_token");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            Console.WriteLine("--> Auth Message Handler Called");

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
