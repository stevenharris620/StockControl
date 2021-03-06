using AKSoftware.Blazor.Utilities;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using StockControl.Services;
using StockControl.Services.Exceptions;
using StockControl.Shared.Requests;

namespace StockControl.Components
{
    public partial class SupplierList
    {
        [Inject] public ISupplierService SupplierService { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Inject] public IDialogService DialogService { get; set; }
        [CascadingParameter] public IModalService Modal { get; set; }

        public bool _loading = true;

        private List<SupplierDetail> _suppliers = new();

        private string _errorMessage = string.Empty;
        private bool _isBusy = false;
        private int _pageNumber = 1;
        private int _pageSize = 10;
        private string _query = string.Empty;
        private int _totalPages = 1;

        private async Task AddSupplier()
        {
            _loading = false;
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraExtraLarge, DisableBackdropClick = true };

            var dialog = DialogService.Show<SupplierForm>("Add a supplier", options);
            var result = await dialog.Result;

            Console.WriteLine(result.Cancelled ? "Modal was cancelled" : "Modal was closed");
            MessagingCenter.Send(this, "supplier_updated", new SupplierDetail());
        }

        private async Task EditSupplier(SupplierDetail supplier)
        {
            _loading = false;

            var parameters = new DialogParameters { { nameof(supplier.Id), supplier.Id } };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraExtraLarge, DisableBackdropClick = true };
            var dialog = DialogService.Show<SupplierWrapper>("Edit a supplier", parameters, options);
            var result = await dialog.Result;

            Console.WriteLine(result.Cancelled ? "Modal was cancelled" : "Modal was closed");
            MessagingCenter.Send(this, "supplier_updated", supplier);
        }

        private async void DeleteSupplierAsync(SupplierDetail zone)
        {
            _loading = false;
            var parameters = new DialogParameters
            {
                { "ContentText", $"Do you really want to delete this zone {zone.Name} This process cannot be undone." },
                { "ButtonText", "Delete" },
                { "Color", Color.Error }
            };

            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

            var dialog = DialogService.Show<ConfirmationDialog>("Delete", parameters, options);
            var result = await dialog.Result;

            try
            {
                if (result.Cancelled) return;
                await SupplierService.DeleteAsync(zone.Id);

                MessagingCenter.Send(this, "supplier_updated", zone);
            }
            catch (ApiException ex)
            {
                _errorMessage = ex.ApiErrorResponse.Message;
            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
            }
        }

    }
}