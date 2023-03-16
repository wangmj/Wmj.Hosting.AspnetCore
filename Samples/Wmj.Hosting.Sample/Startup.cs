using Microsoft.Extensions.DependencyInjection;
using Wmj.Hosting.AspnetCore;
using Wmj.Hosting.AspnetCore.Endpoints;
using Wmj.Hosting.AspnetCore.LifeTimes;
using Wmj.Hosting.AspnetCore.MiddleWares;
using Wmj.Hosting.Sample.Filters;
using Wmj.Hosting.Sample.HealthCheck;

namespace Wmj.Hosting.Sample
{
    internal class Startup
    {
        public static void ConfigService(IServiceCollection service)
        {
            service.AddControllers();
            service.AddAuthenticationCore();
            service.AddAuthorizationCore();
            service.AddScoped<ActionValidateFilter>();
            service.AddHealthChecks().AddCheck<AppHealthCheck>("app");
        }

        public static void BuildService(WebApplication app, IWebHostEnvironment env)
        {
            app.UseMiddleware<RequestErrorHandlerMiddleware>();
            app.UseHealthChecks("/health");
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(routeBuilder =>
            {
                routeBuilder.Map("well-known/app", AppEndpoint.GetAppInfo);
                //routeBuilder.MapControllers();
                routeBuilder.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
              );
            });
        }
    }
}
