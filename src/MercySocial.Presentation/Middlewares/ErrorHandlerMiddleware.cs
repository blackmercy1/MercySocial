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
            // const string message = "An unhandled exception has occurred while executing the request.";
            // _logger.LogError(exception, message);
            //
            // context.Response.Clear();
            // context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            var result = new { error = exception.Message };
            await context.Response.WriteAsJsonAsync(result);
        }
    }
}