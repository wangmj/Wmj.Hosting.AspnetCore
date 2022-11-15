using Microsoft.AspNetCore.Mvc.Filters;

using System.ComponentModel.DataAnnotations;

namespace Wmj.Hosting.Sample.Filters
{
    public class LogActionFilter : ActionFilterAttribute
    {
        private Stream originResponseStream;
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            var response = context.HttpContext.Response;
            originResponseStream = response.Body;
            context.HttpContext.Response.Body = new MemoryStream();


        }
        public override void OnResultExecuted(ResultExecutedContext context)
        {
            var result= context.Result;
            result.ExecuteResultAsync(context);
            base.OnResultExecuted(context);
            //context.Result.
        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);

            string responseContent;
            var response = context.HttpContext.Response;
            var hasStarted = response.HasStarted;
            response.OnStarting(async (currentRes) =>
            {
                var res = currentRes as HttpResponse;
                var resStream = res.Body;
                var canRead= resStream.CanRead;
                resStream.Seek(0, SeekOrigin.Begin);
                responseContent = new StreamReader(resStream).ReadToEndAsync().Result;
                resStream.Seek(0, SeekOrigin.Begin);
                await resStream.CopyToAsync(originResponseStream);
                response.Body = originResponseStream;
            }, response);
        }
    }
}
