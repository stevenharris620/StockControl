using Microsoft.AspNetCore.Identity;
using StockControl.API.Models;

namespace StockControl.API.Repositories;

public interface IUnitOfWork
{
    IUserRepository Users { get; }
}

/// <summary>
/// Entity Framework UoW
/// </summary>
public class EfUnitOfWork : IUnitOfWork
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ApplicationDbContext _db;

    private IdentityUserRepository _users;
    private SupplierRepository _suppliers;

    public EfUnitOfWork(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,
        ApplicationDbContext db)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _db = db;
    }

    public IUserRepository Users => _users ??= new IdentityUserRepository(_userManager, _roleManager);
    public ISupplierRepository Suppliers => _suppliers ??= new SupplierRepository();

    public async Task CommitChangesAsync(string userId)
    {
        await _db.SaveChangesAsync(userId);
    }
}