using Microsoft.AspNetCore.Components;
using MudBlazor;
using StockControl.Services.Exceptions;

namespace StockControl.Shared
{
    public partial class Error
    {
        [Parameter] public RenderFragment ChildContent { get; set; }

        [Inject] public ISnackbar Snackbar { get; set; }

        public void HandleError(Exception ex)
        {
            switch (ex)
            {
                case ApiException except:
                    Snackbar.Configuration.SnackbarVariant = Variant.Filled;
                    Snackbar.Configuration.RequireInteraction = true;
                    Snackbar.Add("Something went wrong in BackEnd" + except.ApiErrorResponse.Message, Severity.Error);
                    break;
                case not null:
                    Snackbar.Configuration.SnackbarVariant = Variant.Filled;
                    Snackbar.Configuration.RequireInteraction = true;
                    Snackbar.Add("Something went wrong in Front End", Severity.Error);
                    break;
            }


            Console.WriteLine($"{ex.Message} at {DateTime.Now}");

        }
    }
}