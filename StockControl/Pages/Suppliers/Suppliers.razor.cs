using MudBlazor;

namespace StockControl.Pages.Suppliers
{
    public partial class Suppliers
    {
        private List<BreadcrumbItem> _breadcrumbItems = new List<BreadcrumbItem>()
        {
            new BreadcrumbItem("Home","/"),
            new BreadcrumbItem("Suppliers","/suppliers",true)
        };
    }
}