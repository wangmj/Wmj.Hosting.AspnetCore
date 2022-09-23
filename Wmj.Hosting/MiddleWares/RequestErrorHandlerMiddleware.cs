using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Wmj.Hosting.AspnetCore.MiddleWares
{
    public class RequestErrorHandlerMiddleware
    {
        private RequestDelegate _next;
        public RequestErrorHandlerMiddleware(RequestDelegate next)
        {
            this._next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var logger = context.RequestServices.GetService<ILogger<RequestErrorHandlerMiddleware>>();
                logger?.LogError(ex, "Occured unhanlded exception");
                var response = context.Response;
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await response.WriteAsync(ex.Message);
            }
        }
    }
}
