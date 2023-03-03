using Braintree.Exceptions;
using SkyDrive.Payment.Models;
using SkyDrive.Payment.Services.Exceptions;
using System.Net;
using System.Text.Json;

namespace SkyDrive.Payment.Middleware
{
    public class ExceptionHandler
    {
        private readonly RequestDelegate _next;

        public ExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                await HandleException(context, exception);
            }
        }

        private static Task HandleException(HttpContext context, Exception exception)
        {
            var statusCode = exception switch
            {
                ResultException => (int)HttpStatusCode.BadRequest,
                NotFoundException => (int)HttpStatusCode.NotFound,
                _ => (int)HttpStatusCode.InternalServerError
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            var result = JsonSerializer.Serialize(new ExceptionModel
            {
                StatusCode = statusCode,
                Message = exception.Message
            });

            return context.Response.WriteAsync(result);
        }
    }
}
