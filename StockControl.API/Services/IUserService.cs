using Microsoft.IdentityModel.Tokens;
using StockControl.API.Infrastucture;
using StockControl.API.Models;
using StockControl.API.Repositories;
using StockControl.Shared.Requests;
using StockControl.Shared.Response;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StockControl.API.Services
{
    public interface IUserService
    {
        Task<RegisterResponse> RegisterUserAsync(RegisterRequest model);
        Task<LoginResponse> GenerateTokenAsync(LoginRequest model);
    }

    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly AuthOptions _authOptions;

        public UserService(IUnitOfWork unitOfWork, AuthOptions authOptions)
        {
            _unitOfWork = unitOfWork;
            _authOptions = authOptions;
        }

        public async Task<RegisterResponse> RegisterUserAsync(RegisterRequest model)
        {
            var userByEmail = await _unitOfWork.Users.GetUserByEmailAsync(model.Email);

            if (userByEmail != null)
                return new RegisterResponse
                {
                    IsSuccess = false,
                    Message = "User already exists"
                };

            var user = new ApplicationUser // TODO mapper for this
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Email
            };

            await _unitOfWork.Users.CreateUserAsync(user, model.Password, "User"); // default new user to User role

            return new RegisterResponse
            {
                Message = "Welcome",
                IsSuccess = true
            };

        }

        public async Task<LoginResponse> GenerateTokenAsync(LoginRequest model)
        {
            var user = await _unitOfWork.Users.GetUserByEmailAsync(model.Email);

            if (user == null)
                return new LoginResponse
                {
                    Message = "Invalid username or password",
                    IsSuccess = false
                };

            if (await _unitOfWork.Users.CheckPasswordAsync(user, model.Password) == false)
                return new LoginResponse
                {
                    Message = "Invalid username or password",
                    IsSuccess = false
                };

            var roles = await _unitOfWork.Users.GetUserRoleAsync(user);

            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim(ClaimTypes.Email, user.Email),
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authOptions.Key));
            var expiry = DateTime.Now.AddDays(30);

            var jwtToken = new JwtSecurityToken(
                _authOptions.Issuer,
                _authOptions.Audience,
                claims,
                expires: expiry,
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            var tokenAsString = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            var response = new LoginResponse
            {
                AccessToken = tokenAsString,
                Message = "Logged on",
                ExpiryDate = expiry,
                IsSuccess = true
            };

            return response;

        }
    }

}
