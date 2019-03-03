using AtiehJobCore.Core.Caching;
using AtiehJobCore.Core.Domain.Tasks;
using AtiehJobCore.Core.Infrastructure;

namespace AtiehJobCore.Services.Tasks
{
    /// <summary>
    /// Clear cache scheduled task implementation
    /// </summary>
    public partial class ClearCacheScheduleTask : ScheduleTask, IScheduleTask
    {
        private readonly object _lock = new object();

        /// <inheritdoc />
        /// <summary>
        /// Executes a task
        /// </summary>
        public void Execute()
        {
            lock (_lock)
            {
                var cacheManagers = EngineContext.Current.ResolveAll<ICacheManager>();
                foreach (var cacheManager in cacheManagers)
                {
                    cacheManager.Clear();
                }
            }
        }
    }
}
