using Wmj.Hosting.MiddleWares;

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

        public static void Config(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<LogMiddleware>();
            app.UseEndpoints(routeBuilder =>
            {
                routeBuilder.MapControllers();
            });
        }
    }
}
