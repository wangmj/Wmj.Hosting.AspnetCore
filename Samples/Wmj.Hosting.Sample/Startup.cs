namespace Wmj.Hosting.Sample
{
    internal class Startup
    {
        public static void CongfigService(IServiceCollection service)
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
            app.UseEndpoints(routeBuilder =>
            {
                routeBuilder.MapControllers();
            });
        }
    }
}
