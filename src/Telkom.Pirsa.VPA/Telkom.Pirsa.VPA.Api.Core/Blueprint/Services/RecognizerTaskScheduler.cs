using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Telkom.Pirsa.VPA.Api.Core.Blueprint.Services
{
    public class RecognizerTaskScheduler : ITaskScheduler, IDisposable
    {
        private readonly BlockingCollection<JobQueueItem> _queue;
        private readonly IList<JobQueueItem> _history;
        private readonly object _lock = new object();
        private readonly SchedulerService _service;
        private readonly ActivityLogService _logger;
        private bool requireStop = false;
        private bool cancelAll;
        private const int WAIT_TIME = 10000; // ms

        public RecognizerTaskScheduler(SchedulerService _scheduler, ActivityLogService logger)
        {
            _queue = new BlockingCollection<JobQueueItem>();
            _service = _scheduler;
            _logger = logger;
            // Start scheduler in background
            Task.Factory.StartNew(() => Execute());
            _logger.LogActivity("Task scheduler running in background at " + DateTime.Now.ToString("dd MMMM yyyy HH:mm:ss"), "Telkom.Pirsa.VPA.Api.Core.Blueprint.Services.RecognizerTaskScheduler");
        }

        public void EnqueueJob(JobQueueItem task)
        {
            lock(_lock)
            {
                _queue.Add(task);
            }
        }

        public void Execute()
        {
            while (!cancelAll)
            {
                if (_queue.Count <= 0)
                {
                    // Wait tasks added
                    Thread.Sleep(WAIT_TIME);
                }
                var task = _queue.Take();
                if (task.HasAction && !task.IsCanceled)
                {
                    _service.ExecuteTask((int)task.ID);
                    task.Execute();
                    _service.FinishTask((int)task.ID);
                }
                // wait cancel signal
                Thread.Sleep(1000);
            }
        }

        public void Cancel(bool allTask = false)
        {
            throw new NotImplementedException();
        }

        public void Clear(bool stopRunning = false)
        {
            throw new NotImplementedException();
        }

        private void Dispose(bool disposing)
        {
            if (!disposing)
                return;
            _queue.CompleteAdding();
            _queue.Dispose();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
