using Microsoft.AspNetCore.Mvc;
using StockControl.API.Mappers;
using StockControl.API.Services;
using StockControl.Shared.Requests;
using StockControl.Shared.Response;

namespace StockControl.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(500, Type = typeof(ApiErrorResponse))]
    public class PartController : ControllerBase
    {
        private readonly IPartService _partService;
        private readonly IPartsMapper _partsMapper;

        public PartController(IPartService partService, IPartsMapper partsMapper)
        {
            _partService = partService;
            _partsMapper = partsMapper;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<PartDetail>))]
        [ProducesResponseType(404, Type = typeof(ApiErrorResponse))]
        public async Task<IActionResult> Get(string id)
        {
            var part = await _partService.GetByIdAsync(id);

            return Ok(new ApiResponse<PartDetail>(_partsMapper.Map_Part_To_PartDetail(part, new PartDetail()),
                "Part Retrieved"
            ));
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(ApiResponse<PartDetail>))]
        [ProducesResponseType(400, Type = typeof(ApiErrorResponse))]
        public async Task<IActionResult> Create(PartDetail model)
        {
            var part = await _partService.CreateAsync(model);

            return Ok(new ApiResponse<PartDetail>(_partsMapper.Map_Part_To_PartDetail(part, new PartDetail()),
                "part Created"));
        }

        [HttpGet("parts")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<PagedList<PartDetail>>))]
        [ProducesResponseType(400, Type = typeof(ApiResponse<PagedList<PartDetail>>))]
        public IActionResult GetAll(string query, int pageNumber, int pageSize)
        {
            var result = _partService.GetPartsAsync(query, pageNumber, pageSize);

            return Ok(new ApiResponse<PagedList<PartDetail>>(result, "Parts retrieved successfully"));
        }

        [HttpPut]
        [ProducesResponseType(200, Type = typeof(ApiResponse<PartDetail>))]
        [ProducesResponseType(400, Type = typeof(ApiErrorResponse))]
        public async Task<IActionResult> Update(PartDetail model)
        {
            var part = await _partService.UpdateAsync(model);

            return Ok(new ApiResponse<PartDetail>(_partsMapper.Map_Part_To_PartDetail(part, new PartDetail()),
                "Part Updated"));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<PartDetail>))]
        [ProducesResponseType(400, Type = typeof(ApiErrorResponse))]
        public async Task<IActionResult> Remove(string id)
        {
            var part = await _partService.RemoveAsync(id);

            return Ok(new ApiResponse<PartDetail>(_partsMapper.Map_Part_To_PartDetail(part, new PartDetail()),
                "Part Deleted"));
        }
    }
}
