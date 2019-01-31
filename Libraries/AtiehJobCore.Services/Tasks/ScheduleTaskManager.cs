using AtiehJobCore.Common.Infrastructure;
using AtiehJobCore.Common.MongoDb.Domain.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace AtiehJobCore.Services.Tasks
{
    public class ScheduleTaskManager
    {
        private ScheduleTaskManager() { }
        public static ScheduleTaskManager Instance { get; } = new ScheduleTaskManager();
        public static List<string> RunningTasks { get; } = new List<string>();

        public List<IScheduleTask> LoadScheduleTasks()
        {
            var scheduleTaskService = EngineContext.Current.Resolve<IScheduleTaskService>();
            var tasks = scheduleTaskService
                .GetAllTasks()
                .ToList();

            var interfaceCollection = new List<IScheduleTask>();
            foreach (var task in tasks)
            {
                var _task = ChangeType(task);
                if (_task != null)
                    interfaceCollection.Add(_task);
            }
            return interfaceCollection;
        }

        public IScheduleTask ChangeType(ScheduleTask scheduleTask)
        {
            IScheduleTask task = null;
            var type2 = System.Type.GetType(scheduleTask.Type);
            if (type2 != null)
            {
                try
                {
                    var instance = EngineContext.Current.Resolve(type2);
                    task = instance as IScheduleTask;
                }
                catch { return null; }
            }

            if (task == null)
            {
                return null;
            }

            task.ScheduleTaskName = scheduleTask.ScheduleTaskName;
            task.Type = scheduleTask.Type;
            task.Enabled = scheduleTask.Enabled;
            task.StopOnError = scheduleTask.StopOnError;
            task.LastStartUtc = scheduleTask.LastStartUtc;
            task.LastNonSuccessEndUtc = scheduleTask.LastNonSuccessEndUtc;
            task.LastSuccessUtc = scheduleTask.LastSuccessUtc;
            task.TimeIntervalChoice = scheduleTask.TimeIntervalChoice;
            task.TimeInterval = scheduleTask.TimeInterval;
            task.MinuteOfHour = scheduleTask.MinuteOfHour;
            task.HourOfDay = scheduleTask.HourOfDay;
            task.DayOfWeek = scheduleTask.DayOfWeek;
            task.MonthOptionChoice = scheduleTask.MonthOptionChoice;
            task.DayOfMonth = scheduleTask.DayOfMonth;
            task.LeasedByMachineName = scheduleTask.LeasedByMachineName;
            return task;
        }
    }
}
