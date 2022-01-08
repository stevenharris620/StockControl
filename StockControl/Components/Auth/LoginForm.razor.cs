using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using StockControl.Shared.Requests;
using StockControl.Shared.Response;
using System.Net.Http.Json;

namespace StockControl.Components
{
    public partial class LoginForm : ComponentBase
    {
        [Inject] public HttpClient HttpClient { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Inject] public AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [Inject] public ILocalStorageService LocalStorageService { get; set; }



        private LoginRequest _model = new();
        private bool _isBusy = false;
        private string _errorMessage = String.Empty;

        private async Task LoginUserAsync()
        {
            _isBusy = true;
            _errorMessage = String.Empty;

            var response = await HttpClient.PostAsJsonAsync("/api/auth/login", _model);

            switch (response.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    var result = await response.Content.ReadFromJsonAsync<ApiResponse<LoginResponse>>();
                    await LocalStorageService.SetItemAsStringAsync("access_token", result.Value.AccessToken);
                    await LocalStorageService.SetItemAsync<DateTime>("expiry_date", (DateTime)result.Value.ExpiryDate);
                    await AuthenticationStateProvider.GetAuthenticationStateAsync();
                    NavigationManager.NavigateTo("/");
                    break;
                case System.Net.HttpStatusCode.BadRequest:
                    var errorResult = await response.Content.ReadFromJsonAsync<ApiResponse<LoginResponse>>();
                    _errorMessage = errorResult.Value.Message;
                    break;
                case System.Net.HttpStatusCode.InternalServerError:
                    break;
            }

            _isBusy = false;
        }

        private void RedirectToRegister()
        {
            NavigationManager.NavigateTo("/authentication/register");
        }
    }

}