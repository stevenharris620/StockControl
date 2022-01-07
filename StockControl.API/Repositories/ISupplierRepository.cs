using StockControl.API.Models;

namespace StockControl.API.Repositories;

public interface ISupplierRepository
{
    Task CreateAsync(Supplier supplier);
    void Remove(Supplier supplier);
    IEnumerable<Supplier> GetAll();
    Task<Supplier?> GetByIdAsync(string id);
}

public class SupplierRepository : ISupplierRepository
{
    private readonly ApplicationDbContext _context;

    public SupplierRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(Supplier supplier)
    {
        await _context.Suppliers!.AddAsync(supplier);
    }

    public void Remove(Supplier supplier)
    {
        _context.Suppliers!.Remove(supplier);
    }

    public IEnumerable<Supplier> GetAll()
    {
        return _context.Suppliers!;
    }

    public async Task<Supplier?> GetByIdAsync(string id)
    {
        return await _context.Suppliers!.FindAsync(id);
    }
}