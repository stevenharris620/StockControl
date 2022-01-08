using Microsoft.AspNetCore.Components;
using StockControl.Shared.Requests;
using StockControl.Shared.Response;
using System.Net.Http.Json;

namespace StockControl.Components
{
    public partial class RegisterForm : ComponentBase
    {
        [Inject] public HttpClient HttpClient { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }

        private RegisterRequest _model = new();
        private bool _isBusy = false;
        private string _errorMessage = String.Empty;

        private async Task RegisterUserAsync()
        {
            _isBusy = true;
            _errorMessage = String.Empty;

            var response = await HttpClient.PostAsJsonAsync("/api/auth/register", _model);

            switch (response.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    var result = await response.Content.ReadFromJsonAsync<ApiResponse<RegisterResponse>>();
                    NavigationManager.NavigateTo("/authentication/login"); // TODO just login user in!
                    break;
                case System.Net.HttpStatusCode.BadRequest:
                    var errorResult = await response.Content.ReadFromJsonAsync<ApiResponse<RegisterResponse>>();
                    _errorMessage = errorResult.Value.Message;
                    break;
                case System.Net.HttpStatusCode.InternalServerError:
                    break;
            }

            _isBusy = false;
        }

        private void RedirectToLogin()
        {
            NavigationManager.NavigateTo("/authentication/login");
        }
    }
}