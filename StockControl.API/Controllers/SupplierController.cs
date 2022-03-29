using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockControl.API.Exceptions;
using StockControl.API.Mappers;
using StockControl.API.Services;
using StockControl.Shared.Requests;
using StockControl.Shared.Response;

namespace StockControl.API.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(500, Type = typeof(ApiErrorResponse))]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierService _supplierService;
        private readonly ISupplierMapper _supplierMapper;

        public SupplierController(ISupplierService supplierService, ISupplierMapper supplierMapper)
        {
            _supplierService = supplierService;
            _supplierMapper = supplierMapper;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<SupplierDetail>))]
        [ProducesResponseType(404, Type = typeof(ApiErrorResponse))]
        public async Task<IActionResult> Get(string id)
        {
            var supplier = await _supplierService.GetByIdAsync(id);

            return Ok(new ApiResponse<SupplierDetail>(_supplierMapper.Map_Supplier_To_SupplierDetail(supplier, new SupplierDetail()),
                "Supplier Retrieved"
            ));
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(ApiResponse<SupplierDetail>))]
        [ProducesResponseType(400, Type = typeof(ApiErrorResponse))]
        public async Task<IActionResult> Create(SupplierDetail model)
        {
            if (!ModelState.IsValid)
            {
                throw new ValidationException($"Validation Error", errors: ModelState.Values.SelectMany(x => x.Errors)
                    .Select(e => e.ErrorMessage)
                    //.Where(y => y.Count > 0)
                    .ToArray());
            }
            var supplier = await _supplierService.CreateAsync(model);

            return Ok(new ApiResponse<SupplierDetail>(_supplierMapper.Map_Supplier_To_SupplierDetail(supplier, new SupplierDetail()),
                "Supplier Created"));
        }

        [HttpGet("suppliers")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<PagedList<SupplierDetail>>))]
        [ProducesResponseType(400, Type = typeof(ApiResponse<PagedList<SupplierDetail>>))]
        public IActionResult GetAll(string? query, int pageNumber, int pageSize)
        {
            var result = _supplierService.GetSuppliersAsync(query, pageNumber, pageSize);

            return Ok(new ApiResponse<PagedList<SupplierDetail>>(result, "Suppliers retrieved successfully"));
        }

        [HttpPut]
        [ProducesResponseType(200, Type = typeof(ApiResponse<SupplierDetail>))]
        [ProducesResponseType(400, Type = typeof(ApiErrorResponse))]
        public async Task<IActionResult> Update(SupplierDetail model)
        {
            var supplier = await _supplierService.UpdateAsync(model);

            return Ok(new ApiResponse<SupplierDetail>(_supplierMapper.Map_Supplier_To_SupplierDetail(supplier, new SupplierDetail()),
                "Supplier Updated"));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<SupplierDetail>))]
        [ProducesResponseType(400, Type = typeof(ApiErrorResponse))]
        public async Task<IActionResult> Remove(string id)
        {
            var supplier = await _supplierService.RemoveAsync(id);

            return Ok(new ApiResponse<SupplierDetail>(_supplierMapper.Map_Supplier_To_SupplierDetail(supplier, new SupplierDetail()),
                "Supplier Deleted"));
        }

    }
}
