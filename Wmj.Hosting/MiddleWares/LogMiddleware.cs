using Microsoft.AspNetCore.Http;

namespace Wmj.Hosting.MiddleWares
{
    public class LogMiddleware
    {
        RequestDelegate _next;
        public LogMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            using var originResponse = context.Response.Body;
            using var ms = new MemoryStream();
            context.Response.Body = ms;
            await _next(context);
            ms.Seek(0, SeekOrigin.Begin);
            var response = new StreamReader(ms).ReadToEndAsync().Result;
            Console.WriteLine(response);
            ms.Seek(0, SeekOrigin.Begin);
            await ms.CopyToAsync(originResponse);
        }
    }
}
