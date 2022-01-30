using Blazored.Modal;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using StockControl.Services;
using StockControl.Shared;
using StockControl.Shared.Requests;

namespace StockControl.Components
{
    public partial class SupplierForm
    {
        //[Inject] public HttpClient HttpClient { get; set; }
        [Inject] public ISupplierService SupplierService { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Inject] ISnackbar Snackbar { get; set; }
        [CascadingParameter] private BlazoredModalInstance ModalInstance { get; set; }
        [CascadingParameter] public Error Error { get; set; }

        [Parameter] public string Id { get; set; }

        private SupplierDetail _model = new();

        private string _errorMessage = string.Empty;
        private bool _isBusy = false;
        private int _pageNumber = 1;
        private int _pageSize = 10;
        private string _query = string.Empty;
        private int _totalPages = 1;

        private bool _isEditMode => !string.IsNullOrEmpty(Id);

        protected override async Task OnInitializedAsync()
        {
            if (_isEditMode) await FetchSupplierByIdAsync();
        }

        private async Task SubmitFormAsync()
        {
            _isBusy = true;

            try
            {
                // throw new Exception("MASSIVE UNEXCEPTED ERROR");
                if (_isEditMode)
                {

                    await SupplierService.EditAsync(_model);
                }
                else
                {
                    _model.Id = String.Empty;
                    var result = await SupplierService.CreateAsync(_model);
                }

                Snackbar.Add("Record Saved", Severity.Success); ;
            }
            catch (Exception ex)
            {
                Error.HandleError(ex);
            }


            _isBusy = false;
        }

        private async Task FetchSupplierByIdAsync()
        {
            _isBusy = true;

            try
            {
                var result = await SupplierService.GetByIdAsync(Id);
                _model = result.Value;
            }
            catch (Exception ex)
            {
                Error.HandleError(ex);
            }

            _isBusy = false;
        }
    }
}