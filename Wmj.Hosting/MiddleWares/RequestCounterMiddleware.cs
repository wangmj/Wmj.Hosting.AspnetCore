using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Wmj.Hosting.MiddleWares
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
                _next(context);
            }
            finally
            {

            }
        }
    }
}
