namespace MercySocial.Presentation.Middlewares
{
    public class GlobalExceptionHandlerMiddleware : IMiddleware
    {
        public GlobalExceptionHandlerMiddleware() { }
        
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                var result = new { error = ex.Message };
                await context.Response.WriteAsJsonAsync(result);
            }
        }
    }
}