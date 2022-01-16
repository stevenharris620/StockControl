using StockControl.API.Exceptions;
using StockControl.API.Infrastucture;
using StockControl.API.Mappers;
using StockControl.API.Models;
using StockControl.API.Repositories;
using StockControl.Shared.Requests;
using StockControl.Shared.Response;

namespace StockControl.API.Services;

public interface IPartService
{
    Task<Part> GetByIdAsync(string id);
    Task<Part> CreateAsync(PartDetail model);
    Task<Part> UpdateAsync(PartDetail model);
    Task<Part> RemoveAsync(string id);

    PagedList<PartDetail> GetPartsAsync(string query = "", int pageNumber = 1, int pageSize = 10);
}

public class PartService : IPartService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPartsMapper _partsMapper;
    private readonly IdentityOptions _identityOptions;

    public PartService(IUnitOfWork unitOfWork, IPartsMapper partsMapper, IdentityOptions identityOptions)
    {
        _unitOfWork = unitOfWork;
        _partsMapper = partsMapper;
        _identityOptions = identityOptions;
    }
    public async Task<Part> GetByIdAsync(string id)
    {
        var part = await _unitOfWork.Parts.GetByIdAsync(id);
        if (part == null) throw new NotFoundException($"Part with Id {id} cannot be found");

        return part;
    }

    public async Task<Part> CreateAsync(PartDetail model)
    {
        var part = _partsMapper.Map_PartDetail_To_Part(model, new Part());

        await _unitOfWork.Parts.CreateAsync(part);
        await _unitOfWork.CommitChangesAsync(_identityOptions.UserId);

        return part;
    }

    public async Task<Part> UpdateAsync(PartDetail model)
    {
        var part = await _unitOfWork.Parts.GetByIdAsync(model.Id);

        if (part == null) throw new NotFoundException($"part with Id {model.Id} cannot be found");

        part = _partsMapper.Map_PartDetail_To_Part(model, part);

        await _unitOfWork.CommitChangesAsync(_identityOptions.UserId);

        return part;
    }

    public async Task<Part> RemoveAsync(string id)
    {
        var part = await _unitOfWork.Parts.GetByIdAsync(id);
        if (part == null) throw new NotFoundException($"Part with Id {id} cannot be found");

        _unitOfWork.Parts.Remove(part);
        await _unitOfWork.CommitChangesAsync(_identityOptions.UserId);

        return part;
    }

    public PagedList<PartDetail> GetPartsAsync(string query = "", int pageNumber = 1, int pageSize = 10)
    {
        if (pageNumber < 1) pageNumber = 1;

        var parts = _unitOfWork.Parts.GetAll().AsQueryable();

        if (!string.IsNullOrEmpty(query))
            parts = parts.Where(c =>
                c.Name!.ToLower().Contains(query.ToLower()) || c.Description!.ToLower().Contains(query.ToLower()));

        var totalRecords = parts.Count();

        parts = parts.OrderBy(x => x.Name);

        var pagedList = new PagedList<PartDetail>(
            parts.Select(p => _partsMapper.Map_Part_To_PartDetail(p, new PartDetail())), pageNumber, pageSize);

        return pagedList;
    }
}
