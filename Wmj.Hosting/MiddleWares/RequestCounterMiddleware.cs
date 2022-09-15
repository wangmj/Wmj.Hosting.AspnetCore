using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Prometheus;

namespace Wmj.Hosting.AspnetCore.MiddleWares
{
    public class RequestCounterMiddleware
    {
        private static readonly Counter requestCounter = Metrics.CreateCounter("Hosting.AspnetCore_request_count", "number of the request");
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
            {// Learn more about configuring Collect-metrics at https://docs.microsoft.com/zh-cn/dotnet/core/diagnostics/metrics-collection
                Meter meter = new Meter(GetType().Name);
                var counter = meter.CreateCounter<int>("http_requests_path_received_total");
                counter.Add(1,
                    KeyValuePair.Create<string, object?>("machine", Environment.MachineName),
                    KeyValuePair.Create<string, object?>("ResponseCode", context.Response.StatusCode),
                    KeyValuePair.Create<string, object?>("RequestMethod", context.Request.Method),
                    KeyValuePair.Create<string, object?>("Uri", context.Request.Path)
                    );
            }
        }
    }
}
