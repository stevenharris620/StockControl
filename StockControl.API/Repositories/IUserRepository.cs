using Microsoft.AspNetCore.Identity;
using StockControl.API.Models;

namespace StockControl.API.Repositories;

public interface IUserRepository
{
    Task<ApplicationUser> GetUserByIdAsync(string id);
    Task<ApplicationUser> GetUserByEmailAsync(string email);
    Task CreateUserAsync(ApplicationUser user, string password, string role);
    Task<bool> CheckPasswordAsync(ApplicationUser user, string password);
    Task<IList<string>> GetUserRoleAsync(ApplicationUser user);
}

public class IdentityUserRepository : IUserRepository
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public IdentityUserRepository(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }
    public async Task CreateUserAsync(ApplicationUser user, string password, string role)
    {
        await _userManager.CreateAsync(user, password);
        await _userManager.AddToRoleAsync(user, role);
    }
    public async Task<bool> CheckPasswordAsync(ApplicationUser user, string password)
        => await _userManager.CheckPasswordAsync(user, password);

    public async Task<IList<string>> GetUserRoleAsync(ApplicationUser user)
        => await _userManager.GetRolesAsync(user);

    public async Task<ApplicationUser> GetUserByEmailAsync(string email)
        => await _userManager.FindByEmailAsync(email);

    public async Task<ApplicationUser> GetUserByIdAsync(string id)
        => await _userManager.FindByIdAsync(id);
}