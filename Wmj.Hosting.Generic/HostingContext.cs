using Microsoft.Extensions.Hosting;
using NLog;

namespace Wmj.Hosting.Generic
{
    public class HostingContext
    {
        public static string NODE_NAME = Environment.MachineName;
        public static void ApplicationInitWithNLog(Action<IHostBuilder> builderAction, string nlogConfigFile = "nlog.config")
        {
            var logger = LogManager.Setup().LoadConfigurationFromFile(nlogConfigFile)
                .GetCurrentClassLogger();
            try
            {
                var builder = Host.CreateDefaultBuilder();
                builderAction(builder);
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