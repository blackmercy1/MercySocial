namespace MercySocial.Presentation.Middlewares;

public class ErrorHandlerMiddleware : IMiddleware
{
    private readonly ILogger<ErrorHandlerMiddleware> _logger;

    public ErrorHandlerMiddleware(ILogger<ErrorHandlerMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            const string message = "An unhandled exception occurred while processing the request.";
            _logger.LogError(exception, message);
            
            context.Response.Clear();
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            var result = new { error = "An error occurred while processing your request. Please try again later." };
            await context.Response.WriteAsJsonAsync(result);
        }
    }
}