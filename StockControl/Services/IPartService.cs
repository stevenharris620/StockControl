using StockControl.Services.Exceptions;
using StockControl.Shared.Helpers;
using StockControl.Shared.Requests;
using StockControl.Shared.Response;
using System.Net.Http.Json;

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
        private readonly HttpClient _httpClient;

        public PartService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ApiResponse<PagedList<PartDetail>>> GetPlayersAsync(string query, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                if (pageNumber < 1) pageNumber = 1;
                query = "a";
                var response = await _httpClient.GetAsync($"api/part/parts?query={query}&pageNumber={pageNumber}&pageSize={pageSize}");



                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<ApiResponse<PagedList<PartDetail>>>();
                    return result;
                }
                else
                {
                    var errorResponse = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                    throw new ApiException(errorResponse, response.StatusCode);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
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
