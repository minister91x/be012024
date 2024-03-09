namespace Eshop.API
{
    public class CustomMiddleWare
    {
        private readonly RequestDelegate _next;

        public CustomMiddleWare(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
           await context.Response.WriteAsync("Hello world!");

            // Call the next delegate/middleware in the pipeline.
            // await _next(context);
        }
    }
}
