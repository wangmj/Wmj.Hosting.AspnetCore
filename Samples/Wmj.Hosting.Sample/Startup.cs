using Wmj.Hosting.AspnetCore;
using Wmj.Hosting.AspnetCore.Endpoints;
using Wmj.Hosting.AspnetCore.LifeTimes;

namespace Wmj.Hosting.Sample
{
    internal class Startup
    {
        public static void ConfigService(IServiceCollection service)
        {
            service.AddControllers();
            service.AddAuthenticationCore();
            service.AddAuthorizationCore();
        }

        public static void BuildService(WebApplication app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.Map("well-known/app",AppEndpoint.GetAppInfo);
            //app.Map("/well-known/app", (builder) => builder.UseMiddleware<AppEndpointsMiddleware>());
            app.UseEndpoints(routeBuilder =>
            {
                routeBuilder.MapControllers();
            });
        }
    }
}
