using Dsw2026Ej15.Domain.Exceptions;

namespace Dsw2026Ej15.Api.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // intenta que la request siga normal
                await _next(context);
            }
            catch (ValidationException ex)
            {
                // si lanza ValidationException → 400 Bad Request
                context.Response.StatusCode = 400;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(ex.Message);
            }
            catch (Exception ex)
            {
                // cualquier otra excepción → 500
                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync("Ocurrió un error interno");
            }
        }
    }
}
