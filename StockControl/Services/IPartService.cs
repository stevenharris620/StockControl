using StockControl.Shared.Helpers;
using StockControl.Shared.Requests;
using StockControl.Shared.Response;

namespace StockControl.Services
{
    public interface IPartService
    {
        Task<ApiResponse<PagedList<PartDetail>>> GetPlayersAsync(string query = null, int pageNumber = 1, int pageSize = 10);
        Task<ApiResponse<PartDetail>> GetByIdAsync(string id);
        Task<ApiResponse<PartDetail>> CreateAsync(PartDetail playerDetail, FormFile image);
        Task<ApiResponse<PartDetail>> EditAsync(PartDetail playerDetail, FormFile image);
        Task<ApiResponse<PartDetail>> DeleteAsync(string id);
    }
    public class PartService : IPartService
    {
        public Task<ApiResponse<PagedList<PartDetail>>> GetPlayersAsync(string query = null, int pageNumber = 1, int pageSize = 10)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<PartDetail>> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<PartDetail>> CreateAsync(PartDetail playerDetail, FormFile image)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<PartDetail>> EditAsync(PartDetail playerDetail, FormFile image)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<PartDetail>> DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
