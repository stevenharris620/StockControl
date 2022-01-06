using StockControl.Shared.Response;
using System.Net;
using System.Text.Json;

namespace StockControl.API.Exceptions.CustomExceptionMiddleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                // TODO - log to DB
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;
            var guid = Guid.NewGuid().ToString();

            var result = new ApiErrorResponse();

            switch (exception)
            {
                //TODO: all exceptions caused by the user
                case NotSupportedException _:
                case AlreadyExistsException _:
                case ValidationException _:
                case NotFoundException _:
                    code = HttpStatusCode.NotFound;
                    result = new ApiErrorResponse(exception.Message);
                    break;
                case { } e:
                    code = HttpStatusCode.InternalServerError;
                    result = string.IsNullOrWhiteSpace(e.Message)
                        ? new ApiErrorResponse("Error")
                        : new ApiErrorResponse(e.Message);
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            var jsonResponse = JsonSerializer.Serialize(result);

            await context.Response.WriteAsync(jsonResponse);
        }
    }
}
