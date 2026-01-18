using CommonModule.Models;
using Microsoft.AspNetCore.Diagnostics;
using FluentValidation;


namespace BeymenCaseStudy.WebAPI.Middlewares
{
    public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            logger.LogError(exception, "An unhandled exception occurred: {Message}", exception.Message);

            var (statusCode, message , errors) = exception switch
            {
                UnauthorizedAccessException => (StatusCodes.Status401Unauthorized, "Unauthorized access",null),
                KeyNotFoundException => (StatusCodes.Status404NotFound, "Resource not found",null),
                ArgumentException => (StatusCodes.Status400BadRequest, "Invalid input provided",null),
                DomainException => (StatusCodes.Status422UnprocessableEntity, "Business logic violation",null),
                ValidationException valEx => (StatusCodes.Status400BadRequest,"Validation failed", valEx.Errors.Select(e => e.ErrorMessage).ToList()),
                _ => (StatusCodes.Status500InternalServerError, "An unexpected error occurred on the server",null)
            };

            httpContext.Response.StatusCode = statusCode;

            var response = new ErrorResponse(statusCode, message, exception.Message);

            await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);

            return true; 
        }
    }
}
