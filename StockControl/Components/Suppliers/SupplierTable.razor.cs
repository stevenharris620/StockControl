using AKSoftware.Blazor.Utilities;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using StockControl.Services;
using StockControl.Shared.Requests;

namespace StockControl.Components
{
    public partial class SupplierTable
    {
        [Inject] public HttpClient HttpClient { get; set; }
        [Inject] public ISupplierService SupplierService { get; set; }

        [Parameter]
        public EventCallback<SupplierDetail> OnViewClicked { get; set; }

        [Parameter]
        public EventCallback<SupplierDetail> OnEditClicked { get; set; }

        [Parameter]
        public EventCallback<SupplierDetail> OnDeleteClicked { get; set; }

        private string? _query = null;
        private MudTable<SupplierDetail> _table;

        protected override void OnInitialized()
        {
            MessagingCenter.Subscribe<SupplierList, SupplierDetail>(this, "supplier_updated", async (sender, args) =>
            {
                await _table.ReloadServerData();
                StateHasChanged();
            });
        }

        private async Task<TableData<SupplierDetail>> ServerReloadAsync(TableState state)
        {

            var result = await SupplierService.GetSuppliersAsync(_query, state.Page + 1, state.PageSize);

            return new TableData<SupplierDetail>
            {
                Items = result.Value.Records,
                TotalItems = result.Value.ItemsCount
            };
        }

        private void OnSearch(string? query)
        {
            _query = query;
            _table.ReloadServerData();
        }
    }
}