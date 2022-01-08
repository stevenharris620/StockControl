using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace StockControl
{
    // nuget - Microsoft.AspNetCore.Components.Authorization
    // nuget - System.IdentityModel.Tokens.Jwt
    public class JwtAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorageService;

        public JwtAuthenticationStateProvider(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            if (await _localStorageService.ContainKeyAsync("access_token"))
            {
                // user logged in 

                var tokenAsString = await _localStorageService.GetItemAsStringAsync("access_token");
                var tokenHandler = new JwtSecurityTokenHandler();

                var token = tokenHandler.ReadJwtToken(tokenAsString);

                var identity = new ClaimsIdentity(token.Claims, "Bearer");
                var user = new ClaimsPrincipal(identity);

                var authState = new AuthenticationState(user);

                NotifyAuthenticationStateChanged(Task.FromResult(authState)); // broadcast auth state - blazor components can subscribe to this

                return authState;
            }

            return new AuthenticationState(new System.Security.Claims.ClaimsPrincipal()); // empty claimsPrincipal means user not logged in
        }
    }
}
