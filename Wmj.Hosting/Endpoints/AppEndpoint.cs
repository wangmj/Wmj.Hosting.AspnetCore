using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace Wmj.Hosting.AspnetCore.Endpoints
{
    public class AppEndpoint
    {
        public static async Task GetAppInfo(HttpContext context)
        {
            Dictionary<string, object> info = new Dictionary<string, object>();
            info.Add("OS Version", Environment.OSVersion.ToString()); 
            info.Add("MachineName", Environment.MachineName);
            info.Add("RunAsUser", Environment.UserName);
            info.Add("RuntimeDirectory", Environment.CurrentDirectory);
            info.Add("AppStatus", HostingContext.CurrentStatus.ToString());
            info.Add("DateTime", DateTime.Now.ToString());
            context.Response.StatusCode = 200;
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.WriteIndented=true;
            await context.Response.WriteAsJsonAsync(info,options);
        }

    }
}
