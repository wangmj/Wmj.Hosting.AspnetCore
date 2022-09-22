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
            app.UseEndpoints(routeBuilder =>
            {
                routeBuilder.MapControllers();
            });
        }
    }
}
