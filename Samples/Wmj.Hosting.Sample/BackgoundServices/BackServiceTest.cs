namespace Wmj.Hosting.Sample.BackgoundServices
{
    public class BackServiceTest : IHostedService
    {
        ILogger<BackServiceTest> logger;

        public BackServiceTest(ILogger<BackServiceTest> logger)
        {
            this.logger = logger;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
           Task.Run(() =>
            {
                while (true)
                {
                    logger.LogInformation("Back Service running...");
                    Task.Delay(2000).Wait();
                }
            });
            return Task.CompletedTask;
           
        }
        public async Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("stopping...");
            await Task.Delay(1000);
            logger.LogInformation("stopped..");
        }
    }
}
