using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Wmj.Hosting.Sample.HealthCheck
{
    public class AppHealthCheck : IHealthCheck
    {
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            await Task.Delay(1000);
            //example:check sql, etc..,
            return HealthCheckResult.Healthy();
        }
    }
}
