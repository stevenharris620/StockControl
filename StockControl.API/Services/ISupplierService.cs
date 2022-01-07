using StockControl.API.Models;
using StockControl.Shared.Requests;
using StockControl.Shared.Response;

namespace StockControl.API.Services;

public interface ISupplierService
{
    Task<Supplier> GetByIdAsync(string id);
    Task<Supplier> CreateAsync(SupplierDetail model);
    Task<Supplier> UpdateAsync(SupplierDetail model);
    Task<Supplier> RemoveAsync(string id);

    PagedList<SupplierDetail> GetSuppliersAsync(string query, int pageNumber = 1, int pageSize = 10);
}

public class SupplierService : ISupplierService
{
    public Task<Supplier> CreateAsync(SupplierDetail model)
    {
        throw new NotImplementedException();
    }

    public Task<Supplier> GetByIdAsync(string id)
    {
        throw new NotImplementedException();
    }

    public PagedList<SupplierDetail> GetSuppliersAsync(string query, int pageNumber = 1, int pageSize = 10)
    {
        throw new NotImplementedException();
    }

    public Task<Supplier> RemoveAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<Supplier> UpdateAsync(SupplierDetail model)
    {
        throw new NotImplementedException();
    }
}
