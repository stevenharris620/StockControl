using AKSoftware.Blazor.Utilities;
using Blazored.Modal;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using StockControl.Services;
using StockControl.Services.Exceptions;
using StockControl.Shared.Requests;

namespace StockControl.Components
{
    public partial class SupplierForm
    {
        //[Inject] public HttpClient HttpClient { get; set; }
        [Inject] public ISupplierService SupplierService { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }
        [CascadingParameter] private BlazoredModalInstance ModalInstance { get; set; }

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

                if (_isEditMode)
                {

                    await SupplierService.EditAsync(_model);
                }
                else
                {
                    _model.Id = String.Empty;
                    // var response = await HttpClient.PostAsJsonAsync("api/Supplier", _model);
                    //_model.Id = String.Empty;
                    var result = await SupplierService.CreateAsync(_model);
                }

                await ModalInstance.CloseAsync(ModalResult.Ok(_model));
                MessagingCenter.Send(this, "supplier_updated", _model);
                NavigationManager.NavigateTo("/suppliers");
            }
            catch (ApiException ex)
            {
                _errorMessage = ex.ApiErrorResponse.Message;
            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
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
                _errorMessage = ex.Message;
            }

            _isBusy = false;
        }
    }
}