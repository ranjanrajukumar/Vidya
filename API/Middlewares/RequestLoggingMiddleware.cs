using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Vidya.API.Middlewares
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Log the request here
            Console.WriteLine($"Request: {context.Request.Method} {context.Request.Path}");
            await _next(context); // Pass the request to the next middleware
        }
    }
}
