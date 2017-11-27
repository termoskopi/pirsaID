using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Telkom.Pirsa.VPA.Api.Core.Blueprint.Services
{
    public class RecognizerService
    {
        private readonly DataAccessManager _manager;
        private readonly ITaskScheduler _scheduler;
        private readonly SchedulerService _service;

        public RecognizerService()
        { 
        
        }

        public RecognizerService(DataAccessManager manager, ITaskScheduler scheduler, SchedulerService service)
        {
            _manager = manager;
            _scheduler = scheduler;
            _service = service;
        }

        public bool Capture(string user, string video)
        {
            try
            {
                JobQueueItem task = new JobQueueItem("Capture Video", string.Format("{0} request capture video {1}", user, video));
                task.SetAction(() => CaptureAction());
                _scheduler.EnqueueJob(task);
                int taskId = _service.CreateTask(TaskType.Capture, (int)task.ID, user, video);
                return _service.EnqueueTask(taskId);
            }
            catch (Exception ex)
            {
                throw ex;            
            }
        }


        public bool Train(string user)
        {
            try
            {
                JobQueueItem task = new JobQueueItem("Train Model", string.Format("{0} request train model", user));
                task.SetAction(() => TrainingAction());
                _scheduler.EnqueueJob(task);
                int taskId = _service.CreateTask(TaskType.Training, (int)task.ID, user);
                return _service.EnqueueTask(taskId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Test(string user, string video)
        {
            try
            {
                JobQueueItem task = new JobQueueItem("Test Video", string.Format("{0} request test video {1}", user, video));
                task.SetAction(() => TestAction());
                _scheduler.EnqueueJob(task);
                int taskId = _service.CreateTask(TaskType.Recognize, (int)task.ID, user, video);
                return _service.EnqueueTask(taskId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IList<string> GetImageResults()
        {
            return null;
        }

        #region Dummy Scheculer Actions
        public void CaptureAction()
        {
            // Wait 10 minutes
            Thread.Sleep(1000 * 60 * 10);
        }

        public void TestAction()
        {
            // Wait 5 minutes
            Thread.Sleep(1000 * 60 * 5);
        }

        public void TrainingAction()
        {
            // Wait 3 minutes
            Thread.Sleep(1000 * 60 * 3);
        }

        #endregion



    }
}
