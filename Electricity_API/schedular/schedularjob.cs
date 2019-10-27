namespace Electricity_API.schedular
{
    using Microsoft.Extensions.Logging;
    using Quartz;
    using System.Threading.Tasks;

    [DisallowConcurrentExecution]
    public class SchedularJob : IJob
    {
        private readonly ILogger<SchedularJob> _logger;
        public SchedularJob(ILogger<SchedularJob> logger)
        {
            _logger = logger;
        }

        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("Schedular Service Called!");
            return Task.CompletedTask;
        }
    }
}
