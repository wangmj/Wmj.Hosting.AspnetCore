using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

using System.Diagnostics;

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
            Stopwatch sw = new Stopwatch();
            sw.Start();
            var request = context.Request;
            request.EnableBuffering();
            RequestLogModel log = new RequestLogModel();
            log.RequestDateTime = DateTime.Now;
            log.RequestMethod = request.Method;
            log.RequestPath = request.Path;
            log.RequestQuery = request.QueryString.ToString();
            log.ReqeustOrigin = request.Host.Value; ;
            log.RequestContent = new StreamReader(request.Body).ReadToEndAsync().Result;
            request.Body.Seek(0, SeekOrigin.Begin);

            using var originResponse = context.Response.Body;
            using var ms = new MemoryStream();
            context.Response.Body = ms;

            await _next(context);

            ms.Seek(0, SeekOrigin.Begin);

            log.ResponseContent = new StreamReader(ms).ReadToEndAsync().Result;
            log.ResponseDateTime = DateTime.Now;
            sw.Stop();
            log.Duration = sw.Elapsed;

            ms.Seek(0, SeekOrigin.Begin);
            await ms.CopyToAsync(originResponse);

            var logger = context.RequestServices.GetService(typeof(ILogger<LogMiddleware>));
            if (logger != null)
            {
                var loger = logger as ILogger<LogMiddleware>;
                loger?.LogTrace(log.ToString());
            }
        }
        private class RequestLogModel
        {
            public string RequestPath { get; set; } = string.Empty;
            public string RequestQuery { get; set; } = string.Empty;
            public string RequestMethod { get; set; } = string.Empty;
            public string RequestContent { get; set; } = string.Empty;
            public DateTime RequestDateTime { get; set; } = DateTime.Now;
            public string ReqeustOrigin { get; set; } = string.Empty;
            public string ResponseContent { get; set; } = string.Empty;
            public string ResponseStatusCode { get; set; } = string.Empty;
            public DateTime? ResponseDateTime { get; set; } = DateTime.Now;
            public TimeSpan Duration { get; set; } = TimeSpan.Zero;
            public override string ToString()
            {
                return System.Text.Json.JsonSerializer.Serialize(this);
            }
        }
    }
}
