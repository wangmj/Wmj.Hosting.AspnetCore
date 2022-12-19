using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Wmj.Hosting.AspnetCore.LifeTimes
{
    public static class BuildinLifeTimeExtension
    {
        public static IApplicationBuilder UseBuildinLifeTimes(this IApplicationBuilder app, IHostApplicationLifetime lifetime)
        {
            var logger = app.ApplicationServices.GetService<ILogger<HostingContext>>();

            lifetime.ApplicationStarted.Register(() =>
            {
                HostingContext.UpdateAppStatus(AppStatus.Started);
                logger?.LogInformation("Application started");
            });
            lifetime.ApplicationStopping.Register(() =>
            {
                HostingContext.UpdateAppStatus(AppStatus.Stopping);
                logger?.LogInformation("Application stopping");
            });
            lifetime.ApplicationStopped.Register(() =>
            {
                HostingContext.UpdateAppStatus(AppStatus.Stopped);
                logger?.LogInformation("Application stopped");
            });

            app.UseEndpoints(route =>
            {
                route.Map("well-known/status", context =>
                {
                    context.Response.StatusCode = 200;
                    context.Response.WriteAsync(HostingContext.CurrentStatus.ToString());
                    return Task.CompletedTask;
                });
                route.Map("admin/status-stop", context =>
                {
                    lifetime.StopApplication();
                    context.Response.StatusCode = 200;
                    context.Response.WriteAsync(HostingContext.CurrentStatus.ToString());
                    return Task.CompletedTask;
                });
            });
            return app;
        }
    }
}
