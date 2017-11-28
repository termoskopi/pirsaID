using System;
using Telkom.Pirsa.VPA.Api.Core.Extension;

namespace Telkom.Pirsa.VPA.Api.Core.Blueprint.Services
{
    public class JobQueueItem : IQueueItem
    {
        private long _id;
        private JobStatus _status;
        private readonly string _title;
        private readonly string _description;
        private readonly object _lock = new object();
        private Action _action;

        public JobQueueItem(string title, string description)
        {
            _title = title;
            _description = description;
            _status = JobStatus.Created;
            _id = DateTime.Now.Ticks;
        }

        public long ID
        {
            get { return _id; }
        }

        public JobStatus Status
        {
            get 
            {
                lock (_lock)
                {
                    return _status; 
                }
            }
        }

        public string Title
        {
            get { return _title; }
        }

        public string Description
        {
            get { return _description; }
        }

        public string StatusDescription
        {
            get { return _status.GetDescription(); }
        }

        public void UpdateJob(JobStatus status)
        {
            lock (_lock)
            {
                _status = status;
            }
        }

        public bool IsCanceled
        {
            get
            {
                lock (_lock)
                {
                    return _status == JobStatus.Aborted;
                }
            }
        }

        public bool HasAction
        {
            get { return _action != null; }
        }

        public void SetAction(Action action)
        {
            _action = action;
        }

        public void Execute()
        {
            try
            {
                if (HasAction)
                    _action.Invoke();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
    }
}
