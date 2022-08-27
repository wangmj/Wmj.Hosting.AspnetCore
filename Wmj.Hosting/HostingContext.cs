using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Web;
using Wmj.Infrastruct;

namespace Wmj.Hosting
{
    public class HostingContext
    {
        public static void ApplicationInitWithNLog(Action<WebApplicationBuilder> startAction)
        {
            Guard.ThrowIfNull(startAction, nameof(startAction));

            var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
            logger.Debug("init main");
            try
            {
                var webappBuilder = WebApplication.CreateBuilder();
                webappBuilder.Host.ConfigureLogging(logbuilder =>
                {
                    logbuilder.ClearProviders();
                    logbuilder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                });
                webappBuilder.Host.UseNLog();
                startAction(webappBuilder);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Stopped program because of exception");
                throw;
            }
            finally
            {
                LogManager.Shutdown();
            }
        }
    }
}