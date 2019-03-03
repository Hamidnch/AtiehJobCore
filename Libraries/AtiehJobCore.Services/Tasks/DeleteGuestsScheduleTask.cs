using AtiehJobCore.Core.Domain.Common;
using AtiehJobCore.Core.Domain.Tasks;
using AtiehJobCore.Services.Users;
using System;

namespace AtiehJobCore.Services.Tasks
{
    /// <summary>
    /// Represents a task for deleting guest users
    /// </summary>
    public partial class DeleteGuestsScheduleTask : ScheduleTask, IScheduleTask
    {
        private readonly IUserService _userService;
        private readonly CommonSettings _commonSettings;
        private readonly object _lock = new object();

        public DeleteGuestsScheduleTask(IUserService userService, CommonSettings commonSettings)
        {
            this._userService = userService;
            this._commonSettings = commonSettings;
        }

        /// <inheritdoc />
        /// <summary>
        /// Executes a task
        /// </summary>
        public void Execute()
        {
            lock (_lock)
            {
                var olderThanMinutes = _commonSettings.DeleteGuestTaskOlderThanMinutes;
                // Default value in case 0 is returned.  0 would effectively disable this service and harm performance.
                olderThanMinutes = olderThanMinutes == 0 ? 1440 : olderThanMinutes;
                _userService.DeleteGuestUsers(null, DateTime.UtcNow.AddMinutes(-olderThanMinutes));
            }
        }
    }
}
