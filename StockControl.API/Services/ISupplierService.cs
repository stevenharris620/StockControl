using StockControl.API.Models;
using StockControl.Shared.Requests;

namespace StockControl.API.Services;

public interface ISupplierService
{
    Task<Supplier> GetByIdAsync(string id);
    Task<Supplier> CreateAsync(SupplierDetail model);
    Task<Supplier> UpdateAsync(SupplierDetail model);
    Task<Supplier> RemoveAsync(string id);

    PagedList<SupplierDetail> GetSuppliersAsync(string query, int pageNumber = 1, int pageSize = 10);
}