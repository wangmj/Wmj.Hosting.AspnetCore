using System.Diagnostics.Metrics;
using Microsoft.AspNetCore.Http;

namespace Wmj.Hosting.AspnetCore.MiddleWares
{
    public class RequestCounterMiddleware
    {
        private readonly RequestDelegate _next;
        public RequestCounterMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            finally
            {
                // Learn more about configuring Collect-metrics at https://docs.microsoft.com/zh-cn/dotnet/core/diagnostics/metrics-collection
                Meter meter = new Meter(GetType().Name);
                var counter = meter.CreateCounter<int>("http_requests_path_received_total");
                counter.Add(1,
                    KeyValuePair.Create<string, object?>("Machine", Environment.MachineName),
                    KeyValuePair.Create<string, object?>("ResponseCode", context.Response.StatusCode),
                    KeyValuePair.Create<string, object?>("RequestMethod", context.Request.Method),
                    KeyValuePair.Create<string, object?>("Uri", context.Request.Path)
                    );
            }
        }
    }
}
