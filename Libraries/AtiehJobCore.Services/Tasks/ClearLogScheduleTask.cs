using AtiehJobCore.Core.Domain.Tasks;
using AtiehJobCore.Services.Logging;

namespace AtiehJobCore.Services.Tasks
{
    /// <summary>
    /// Represents a task to clear [Log] table
    /// </summary>
    public partial class ClearLogScheduleTask : ScheduleTask, IScheduleTask
    {
        private readonly ILogger _logger;
        private readonly object _lock = new object();
        public ClearLogScheduleTask(ILogger logger)
        {
            this._logger = logger;
        }

        /// <inheritdoc />
        /// <summary>
        /// Executes a task
        /// </summary>
        public void Execute()
        {
            lock (_lock)
            {
                _logger.ClearLog();
            }
        }
    }
}
