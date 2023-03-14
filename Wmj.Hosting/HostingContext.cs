using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Web;
using System.Collections.Concurrent;
using Wmj.Infrastruct;

namespace Wmj.Hosting.AspnetCore
{
    public class HostingContext
    {
        public static string NODE_NAME = Environment.MachineName;
        
        private static ConcurrentDictionary<string, AppStatus> _statusDict = new ConcurrentDictionary<string, AppStatus>();
        
        internal static void UpdateAppStatus(AppStatus status)
        {
            _statusDict["status"] = status;
        }
        
        public static AppStatus CurrentStatus
        {
            get
            {
                return _statusDict["status"];
            }
            set
            {
                UpdateAppStatus(value);
            }
         }
       
        public static void InitApplicationWithNLog(Action<WebApplicationBuilder> startAction)
        {
            Guard.ThrowIfNull(startAction, nameof(startAction));

            var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
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
    public enum AppStatus
    {
        Starting,
        Started,
        Stopping,
        Stopped,
    }
}