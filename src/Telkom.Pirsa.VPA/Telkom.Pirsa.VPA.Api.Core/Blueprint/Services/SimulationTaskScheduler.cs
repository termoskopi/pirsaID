using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Telkom.Pirsa.VPA.Api.Core.Blueprint.Services
{
    public class SimulationTaskScheduler : ITaskScheduler, IDisposable
    {
        private readonly BlockingCollection<JobQueueItem> _queue;
        private readonly IList<JobQueueItem> _history;
        private readonly object _lock = new object();
        private bool requireStop = false;
        private bool cancelAll;
        private const int ITERATION = 100;
        private const int WAIT_TIME = 10000; // ms

        public SimulationTaskScheduler()
        {
            _queue = new BlockingCollection<JobQueueItem>();
            _history = new List<JobQueueItem>();
        }

        public void EnqueueJob(JobQueueItem task)
        {
            lock(_lock)
            {
                _queue.Add(task);
                _history.Add(task);
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
                    task.Execute();
                    var completedTask = _history.Where(x => task.ID == x.ID).First();
                    

                }
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
