using Telkom.Pirsa.VPA.Api.Core.Blueprint.Services;

namespace Telkom.Pirsa.VPA.Api.Core.Blueprint
{
    /// <summary>
    /// Manages system task during server run. Tasks are run one at a time.
    /// Every time a task finished or canceled, scheduler should begin next waiting task immediately if any
    /// </summary>
    public interface ITaskScheduler
    {
        /// <summary>
        /// Adds a new task to scheduler
        /// </summary>
        /// <param name="task">New task to be added</param>
        void EnqueueJob(JobQueueItem task);

        /// <summary>
        /// Long run task that executes all waiting tasks from the first waiting task on queue
        /// </summary>
        void Execute();

        /// <summary>
        /// Cancels current running task and optionally the rest of waiting tasks as well 
        /// </summary>
        /// <param name="allTask">Optionally stop all waiting tasks, set true to stop all tasks</param>
        void Cancel(bool allTask = false);

        /// <summary>
        /// Clears all waiting tasks and optionally stop current running task
        /// </summary>
        /// <param name="stopRunning">Optionally stop current task while clear all waiting tasks, set true to stop current running task</param>
        void Clear(bool stopRunning = false);

    }
}
