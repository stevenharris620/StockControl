using StockControl.Services.Exceptions;
using StockControl.Shared.Requests;
using StockControl.Shared.Response;
using System.Net.Http.Json;

namespace StockControl.Services;

public interface ISupplierService
{
    Task<ApiResponse<PagedList<SupplierDetail>>> GetSuppliersAsync(string query = null, int pageNumber = 1, int pageSize = 10);
    Task<ApiResponse<SupplierDetail>> GetByIdAsync(string id);
    Task<ApiResponse<SupplierDetail>> CreateAsync(SupplierDetail supplierDetail);
    Task<ApiResponse<SupplierDetail>> EditAsync(SupplierDetail supplierDetail);
    Task<ApiResponse<SupplierDetail>> DeleteAsync(string id);
}

public class SuppliersService : ISupplierService
{
    private readonly HttpClient _httpClient;

    public SuppliersService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ApiResponse<SupplierDetail>> CreateAsync(SupplierDetail supplierDetail)
    {
        var response = await _httpClient.PostAsJsonAsync("api/supplier", supplierDetail);
        return await GetResponse(response);
    }

    public async Task<ApiResponse<SupplierDetail>> DeleteAsync(string id)
    {
        var response = await _httpClient.DeleteAsync($"api/supplier/{id}");
        return await GetResponse(response);
    }

    public async Task<ApiResponse<SupplierDetail>> EditAsync(SupplierDetail supplierDetail)
    {
        var response = await _httpClient.PutAsJsonAsync("api/supplier", supplierDetail);
        return await GetResponse(response);
    }

    public async Task<ApiResponse<SupplierDetail>> GetByIdAsync(string id)
    {
        var response = await _httpClient.GetAsync($"api/supplier/{id}");
        return await GetResponse(response);
    }

    public async Task<ApiResponse<PagedList<SupplierDetail>>> GetSuppliersAsync(string query = null, int pageNumber = 1, int pageSize = 10)
    {
        var response = await _httpClient.GetAsync($"api/supplier/suppliers?query={query}&pageNumber={pageNumber}&pageSize={pageSize}");
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<ApiResponse<PagedList<SupplierDetail>>>();
            return result;
        }
        else
        {
            var errorResponse = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
            throw new ApiException(errorResponse, response.StatusCode);
        }
    }

    private async Task<ApiResponse<SupplierDetail>> GetResponse(HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<ApiResponse<SupplierDetail>>();
            return result;
        }
        else
        {
            var errorResponse = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
            throw new ApiException(errorResponse, response.StatusCode);
        }
    }
}
