using StockControl.Services.Exceptions;
using StockControl.Shared.Helpers;
using StockControl.Shared.Requests;
using StockControl.Shared.Response;
using System.Net.Http.Json;

namespace StockControl.Services
{
    public interface IPartService
    {
        Task<ApiResponse<PagedList<PartDetail>>> GetPartsAsync(string query, int pageNumber = 1, int pageSize = 10);
        Task<ApiResponse<PartDetail>> GetByIdAsync(string id);
        Task<ApiResponse<PartDetail>> CreateAsync(PartDetail playerDetail, FormFile? image);
        Task<ApiResponse<PartDetail>> EditAsync(PartDetail playerDetail, FormFile? image);
        Task<ApiResponse<PartDetail>> DeleteAsync(string id);
    }
    public class PartService : IPartService
    {
        private readonly HttpClient _httpClient;

        public PartService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ApiResponse<PagedList<PartDetail>>> GetPartsAsync(string? query, int pageNumber = 1, int pageSize = 10)
        {

            try
            {
                if (pageNumber < 1) pageNumber = 1;
                var response = await _httpClient.GetAsync($"/api/part/parts?query={query}&pageNumber={pageNumber}&pageSize={pageSize}");

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<ApiResponse<PagedList<PartDetail>>>();
                    //throw new Exception("COunt :- " + result.Value.ItemsCount);
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

        public async Task<ApiResponse<PartDetail>> GetByIdAsync(string id)
        {
            var response = await _httpClient.GetAsync($"/api/part/{id}");
            return await GetResponse(response);
        }

        public async Task<ApiResponse<PartDetail>> CreateAsync(PartDetail partDetail, FormFile? image)
        {

            var form = PreparePartForm(partDetail, image, false);

            var response = await _httpClient.PostAsync("api/part", form);
            return await GetResponse(response);


        }

        public async Task<ApiResponse<PartDetail>> EditAsync(PartDetail partDetail, FormFile? image)
        {
            var form = PreparePartForm(partDetail, image, true);

            var response = await _httpClient.PutAsync("api/part", form);
            return await GetResponse(response);
        }

        private async Task<ApiResponse<PartDetail>> GetResponse(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ApiResponse<PartDetail>>();
                return result;
            }
            else
            {
                var errorResponse = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                throw new ApiException(errorResponse, response.StatusCode);
            }
        }




        public Task<ApiResponse<PartDetail>> DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        private HttpContent PreparePartForm(PartDetail model, FormFile? imageFile, bool isUpdate)
        {
            var form = new MultipartFormDataContent();

            form.Add(new StringContent(model.Name), nameof(PartDetail.Name));


            if (isUpdate)
            {
                form.Add(new StringContent(model.Id), nameof(PartDetail.Id));
            }
            //else
            //{
            //    model.Id = "";
            //    form.Add(new StringContent(model.Id), nameof(PartDetail.Id));
            //}

            if (!string.IsNullOrEmpty(model.PartCode))
                form.Add(new StringContent(model.PartCode), nameof(PartDetail.PartCode));
            //else
            //{
            //    model.PartCode = " ";
            //    form.Add(new StringContent(model.PartCode), nameof(PartDetail.PartCode));
            //}

            if (!string.IsNullOrEmpty(model.Description))
                form.Add(new StringContent(model.Description), nameof(PartDetail.Description));



            form.Add(new StringContent(model.Cost.ToString()), nameof(PartDetail.Cost));

            if (!string.IsNullOrEmpty(model.UnitType))
                form.Add(new StringContent(model.UnitType.ToString()), nameof(PartDetail.UnitType));

            form.Add(new StringContent(model.StockLevel.ToString()), nameof(PartDetail.StockLevel));
            form.Add(new StringContent(model.ReorderLevel.ToString()), nameof(PartDetail.ReorderLevel));



            if (!string.IsNullOrEmpty(model.SupplierId))
                form.Add(new StringContent(model.SupplierId), nameof(PartDetail.SupplierId));

            if (!string.IsNullOrWhiteSpace(model.ImageChar64))
                form.Add(new StringContent(model.ImageChar64), nameof(PartDetail.ImageChar64));

            if (imageFile != null)
                form.Add(new StreamContent(imageFile.FileStream), nameof(model.ThumbFile), imageFile.FileName);


            return form;

        }
    }
}
