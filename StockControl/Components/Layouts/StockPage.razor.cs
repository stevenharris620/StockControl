using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace StockControl.Components
{
    public partial class StockPage
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public string Title { get; set; }
        [Parameter]
        public List<BreadcrumbItem> BreadcrumbItems { get; set; } = new List<BreadcrumbItem>();

    }
}