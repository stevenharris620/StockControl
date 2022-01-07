using StockControl.API.Models;

namespace StockControl.API.Repositories;

public interface IPartRepository
{
    Task CreateAsync(Part part);
    void Remove(Part part);
    IEnumerable<Part> GetAll();
    Task<Part?> GetByIdAsync(string id);
}

public class PartRepository : IPartRepository
{
    private readonly ApplicationDbContext _context;

    public PartRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(Part part)
    {
        await _context.Parts!.AddAsync(part);
    }

    public IEnumerable<Part> GetAll()
    {
        return _context.Parts!;
    }

    public async Task<Part?> GetByIdAsync(string id)
    {
        return await _context.Parts!.FindAsync(id);
    }

    public void Remove(Part part)
    {
        _context.Parts!.Remove(part);
    }
}
