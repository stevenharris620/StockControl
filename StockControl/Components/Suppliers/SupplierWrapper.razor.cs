using Blazored.Modal;
using Microsoft.AspNetCore.Components;

namespace StockControl.Components
{
    public partial class SupplierWrapper
    {
        [Parameter] public string Id { get; set; }
        [CascadingParameter] private BlazoredModalInstance ModalInstance { get; set; }
    }
}