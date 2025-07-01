using DailyQuoteManager.Application.Common.Responses;
using System.Net;
using System.Text.Json;

namespace DailyQuoteManager.Api.Middleware
{
    public class ExceptionMiddleware
        (RequestDelegate _next,
        ILogger<ExceptionMiddleware> _logger)
    {
        #region Public Methods

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred");
                await HandleExceptionAsync(context, ex);
            }
        }

        #endregion Public Methods

        #region Private Methods

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            // Map exceptions to appropriate HTTP status codes
            context.Response.StatusCode = GetStatusCodeFromException(exception);

            var errorResponse = new Response
            {
                Success = false,
                ErrorMessage = GetErrorMessage(exception, context.Response.StatusCode)
            };

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var result = JsonSerializer.Serialize(errorResponse, options);
            await context.Response.WriteAsync(result);
        }

        private static int GetStatusCodeFromException(Exception exception) => exception switch
        {
            ArgumentException or ArgumentNullException => (int)HttpStatusCode.BadRequest,
            KeyNotFoundException => (int)HttpStatusCode.NotFound,
            UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized,
            InvalidOperationException => (int)HttpStatusCode.BadRequest,

            _ => (int)HttpStatusCode.InternalServerError
        };

        private static string GetErrorMessage(Exception exception, int statusCode)
        {
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                return exception.Message;
            }

            return statusCode switch
            {
                (int)HttpStatusCode.BadRequest => "The request was invalid or cannot be served.",
                (int)HttpStatusCode.NotFound => "The requested resource could not be found.",
                (int)HttpStatusCode.Unauthorized => "Authentication is required or has failed.",
                _ => "An unexpected error occurred. Please try again later."
            };
        }

        #endregion Private Methods
    }
}