using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Diagnostics;

namespace messaging_service.Exceptions
{
    public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger = logger;

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, 
            Exception exception,
            CancellationToken cancellationToken)
        {
            var traceId = Activity.Current?.Id ?? httpContext.TraceIdentifier;

            _logger.LogError(
                exception,
                "Could not process a request on machine {MachineName}. TraceId: {TraceId}",
                Environment.MachineName,
                traceId
               );

            var (statusCode, title) = MapException(exception);

            await Results.Problem(
                title: title,
                statusCode: statusCode,
                extensions: new Dictionary<string, object?>
                {
                    {"traceId" , traceId }
                }
                ).ExecuteAsync(httpContext);

            return true;
        }

        private static (int statusCode, string title) MapException(Exception exception)
        {
            return exception switch
            {
                SqlException => (StatusCodes.Status400BadRequest, "Failed To do the operation due to a database error,Check Your Request!"),
                DbUpdateException => (StatusCodes.Status400BadRequest, "Failed To do the operation due to a bad request(One of the Attributes might be already in use or invalid),retry..."),
                ConstraintException => (StatusCodes.Status400BadRequest, "Failed To do the operation due to a violated constraint (PK,FK,Uniqueness,etc...)"),
                TimeoutException => (StatusCodes.Status500InternalServerError, "Failed To do the operation For taking too long."),
                DataException => (StatusCodes.Status400BadRequest, "Failed to do the operation due to invalid data"),
                InvalidOperationException => (StatusCodes.Status500InternalServerError,"Failed Operation due to invalid calls"),
                ValidationException => (StatusCodes.Status400BadRequest,"Failed Operation due to Invalid Request Data"),
                _ => (StatusCodes.Status500InternalServerError, "Internal Server Error"),
            } ;
        }
    }
}
