using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using StockControl.API.Exceptions.CustomExceptionMiddleware;
using StockControl.API.Infrastucture;
using StockControl.API.Mappers;
using StockControl.API.Models;
using StockControl.API.Repositories;
using StockControl.API.Services;
using StockControl.API.Services.Utilities;
using System.Security.Claims;
using System.Text;

namespace StockControl.API.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", p => { p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); });
            });
        }

        public static void AddSwagger(this IServiceCollection services)
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "StockControlAPI", Version = "v1" });
            });
        }
        public static void AddUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, EfUnitOfWork>();
        }

        public static void ConfigureIdentityOptions(this IServiceCollection services)
        {
            services.AddScoped(sp =>
            {
                var httpContext = sp.GetService<IHttpContextAccessor>()?.HttpContext;

                var identityOptions = new Infrastucture.IdentityOptions();

                if (httpContext != null && httpContext.User.Identity.IsAuthenticated)
                {
                    identityOptions.UserId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    identityOptions.FullName =
                        $"{httpContext.User.FindFirst(ClaimTypes.GivenName)?.Value} {httpContext.User.FindFirst(ClaimTypes.Surname)?.Value}";
                    identityOptions.Email = httpContext.User.FindFirst(ClaimTypes.Email)?.Value;

                    identityOptions.IsManager =
                        httpContext.User.FindFirst(ClaimTypes.Role)?.Value == "Manager" ? true : false;
                }

                return identityOptions;
            });
        }

        public static void AddBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ISupplierService, SupplierService>();
            services.AddScoped<IPartService, PartService>();


            services.AddScoped<IImageService, IImageService>();
        }

        public static void AddMappers(this IServiceCollection services)
        {
            services.AddScoped<ISupplierMapper, SupplierMapper>();
            services.AddScoped<IPartsMapper, PartsMapper>();
        }

        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(sp => new AuthOptions
            {
                Audience = configuration["AuthSettings:Audience"],
                Issuer = configuration["AuthSettings:Issuer"],
                Key = configuration["AuthSettings:Key"]
            });

            //services.AddScoped(sp => new EnviromentOptions
            //{
            //    ApiUrl = configuration["ApiUrl"]
            //});
        }



        public static void AddIdentityAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            // Add Identity
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
                {
                    // -- set password options here

                    options.Password.RequireDigit = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequiredLength = 4;

                    // --

                }).AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders(); // adds token for resetting password etc

            // Add Jwt Auth

            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; // set up Bearer
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                // validate token
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = configuration["AuthSettings:Audience"],
                    ValidIssuer = configuration["AuthSettings:Issuer"],
                    RequireExpirationTime = true,
                    IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["AuthSettings:Key"])),
                    ValidateIssuerSigningKey = true
                };
            });
        }

        public static void AddApplicationDatabaseContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        }

        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
