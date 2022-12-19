using Application.Exceptions;
using System.Net;
using System.Text.Json;

namespace PKIK_Project.Helpers
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                response.StatusCode = ex switch
                {
                    AccessForbiddenException => (int)HttpStatusCode.Forbidden,
                    ObjectAlreadyExistsException => (int)HttpStatusCode.BadRequest,
                    ObjectNotFoundException => (int)HttpStatusCode.NotFound,
                    ObjectValidationException => (int)HttpStatusCode.BadRequest,
                    _ => (int)HttpStatusCode.InternalServerError,
                };
                var result = JsonSerializer.Serialize(new { message = ex?.Message });
                await response.WriteAsync(result);
            }
        }
    }
}
