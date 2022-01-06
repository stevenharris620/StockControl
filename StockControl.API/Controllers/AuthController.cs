using Microsoft.AspNetCore.Mvc;
using StockControl.API.Models.Exceptions;
using StockControl.API.Services;
using StockControl.Shared.Requests;
using StockControl.Shared.Response;

namespace StockControl.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(500, Type = typeof(ErrorDetails))]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<LoginResponse>))]
        [ProducesResponseType(400, Type = typeof(ApiResponse<LoginResponse>))]
        public async Task<IActionResult> Login(LoginRequest model)
        {
            //throw new Exception("Testy");
            var result = await _userService.GenerateTokenAsync(model);

            if (result.IsSuccess) return Ok(new ApiResponse<LoginResponse>(result));

            return BadRequest(new ApiResponse<LoginResponse>(result));
        }

        [HttpPost("register")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<RegisterResponse>))]
        [ProducesResponseType(400, Type = typeof(ApiResponse<RegisterResponse>))]
        public async Task<IActionResult> Register(RegisterRequest model)
        {
            var result = await _userService.RegisterUserAsync(model);

            if (result.IsSuccess) return Ok(new ApiResponse<RegisterResponse>(result));

            return BadRequest(new ApiResponse<RegisterResponse>(result));
        }
    }
}
