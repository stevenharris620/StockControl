using StockControl.API.Exceptions;
using StockControl.API.Infrastucture;
using StockControl.API.Mappers;
using StockControl.API.Models;
using StockControl.API.Repositories;
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
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISupplierMapper _supplierMapper;
    private readonly IdentityOptions _identityOptions;

    public SupplierService(IUnitOfWork unitOfWork, ISupplierMapper supplierMapper, IdentityOptions identityOptions)
    {
        _unitOfWork = unitOfWork;
        _supplierMapper = supplierMapper;
        _identityOptions = identityOptions;
    }

    public async Task<Supplier> CreateAsync(SupplierDetail model)
    {
        var supplier = _supplierMapper.Map_SupplierDetail_To_Supplier(model, new Supplier());

        await _unitOfWork.Suppliers.CreateAsync(supplier);
        await _unitOfWork.CommitChangesAsync(_identityOptions.UserId);

        return supplier;
    }

    public async Task<Supplier> GetByIdAsync(string id)
    {
        var supplier = await _unitOfWork.Suppliers.GetByIdAsync(id);
        if (supplier == null) throw new NotFoundException($"Supplier with Id {id} cannot be found");

        return supplier;
    }

    public PagedList<SupplierDetail> GetSuppliersAsync(string query, int pageNumber = 1, int pageSize = 10)
    {
        if (pageNumber < 1) pageNumber = 1;

        var suppliers = _unitOfWork.Suppliers.GetAll().AsQueryable();

        if (!string.IsNullOrEmpty(query))
            suppliers = suppliers.Where(c =>
                c.Name!.ToLower().Contains(query.ToLower()) || c.Email!.ToLower().Contains(query.ToLower()));

        var totalRecords = suppliers.Count();

        suppliers = suppliers.OrderBy(x => x.Name);

        var pagedList = new PagedList<SupplierDetail>(
            suppliers.Select(p => _supplierMapper.Map_Supplier_To_SupplierDetail(p, new SupplierDetail())), pageNumber, pageSize);

        return pagedList;

    }

    public async Task<Supplier> RemoveAsync(string id)
    {
        var supplier = await _unitOfWork.Suppliers.GetByIdAsync(id);
        if (supplier == null) throw new NotFoundException($"Supplier with Id {id} cannot be found");

        _unitOfWork.Suppliers.Remove(supplier);
        await _unitOfWork.CommitChangesAsync(_identityOptions.UserId);

        return supplier;
    }

    public async Task<Supplier> UpdateAsync(SupplierDetail model)
    {
        var supplier = await _unitOfWork.Suppliers.GetByIdAsync(model.Id);

        if (supplier == null) throw new NotFoundException($"Supplier with Id {model.Id} cannot be found");

        supplier = _supplierMapper.Map_SupplierDetail_To_Supplier(model, supplier);

        await _unitOfWork.CommitChangesAsync(_identityOptions.UserId);

        return supplier;
    }
}
