using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Wmj.Hosting.AspnetCore.LifeTimes
{
    public static class BuildinLifeTimeExtension
    {
        public static IApplicationBuilder UseBuildinLifeTimes(this IApplicationBuilder app, IHostApplicationLifetime lifetime)
        {
            var logger= app.ApplicationServices.GetService<ILogger<HostingContext>>();
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
            return app;
        }
    }
}
