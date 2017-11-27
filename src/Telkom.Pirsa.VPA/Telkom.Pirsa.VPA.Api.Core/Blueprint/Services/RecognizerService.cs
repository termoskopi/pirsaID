using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telkom.Pirsa.VPA.Engine.ComputerVision.FaceRecognition;
using Telkom.Pirsa.VPA.Engine.Helper;
using Telkom.Pirsa.VPA.Setting;

namespace Telkom.Pirsa.VPA.Api.Core.Blueprint.Services
{
    public class RecognizerService
    {
        private readonly DataAccessManager _manager;
        private readonly ITaskScheduler _scheduler;
        private readonly SchedulerService _service;
        private readonly ActivityLogService _logger;
        private Recognizer _recognizer;
        private readonly object _lock = new object();
        private Setting.Setting _setting;
        public RecognizerService()
        { 
        
        }

        public RecognizerService(DataAccessManager manager, ITaskScheduler scheduler, SchedulerService service, ActivityLogService logger)
        {
            _manager = manager;
            _scheduler = scheduler;
            _service = service;
            _logger = logger;

            InitializeRecognizer();
        }

        public void InitializeRecognizer()
        {
          try
          {
            lock (_lock)
            {
              _setting = SettingManager.ReadFromFile();
              var recognizerParam = new RecognizerParameter(_setting.RecognizerConfiguration);
              _recognizer = new Recognizer(recognizerParam);
            }
          }
          catch (Exception ex)
          {
            throw ex;
          }
        }

        public Newtonsoft.Json.Linq.JObject GetVideoProperties(string url)
        {
          try
          {
            var property = VideoCapture.LoadVideo(url, SettingManager.ReadFromFile());
            return new Newtonsoft.Json.Linq.JObject()
            {
              new Newtonsoft.Json.Linq.JProperty("Name", property.Name),
              new Newtonsoft.Json.Linq.JProperty("Duration", property.Duration + " s"),
              new Newtonsoft.Json.Linq.JProperty("Size", property.Size + "MB"),
              new Newtonsoft.Json.Linq.JProperty("FrameRate", property.FrameRate + "frame/s")
            };
          }
          catch (Exception ex)
          {
            throw ex;
          }
        }

        public bool Capture(string user, string video, string name)
        {
            try
            {
                JobQueueItem task = new JobQueueItem("Capture Video", string.Format("{0} request capture video {1}", user, video));
                task.SetAction(() => CaptureAction(video, name));
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
        public void CaptureAction(string path, string name)
        {
          _logger.LogSystemActivity(string.Format("Starting capture video {0} with name {1} at {2:dd MMMM yyyy HH:mm}", path, name, DateTime.Now));
          _setting = SettingManager.ReadFromFile();
          var captured = VideoCapture.Capture(path, name, _setting, VideoCapture.CaptureOptions.CaptureAndSave, VideoCapture.CaptureSaveOption.SaveOriginalResized);
          _logger.LogSystemActivity(string.Format("Finished capture video {0} with name {1} at {2:dd MMMM yyyy HH:mm}: {3} frames captured", path, name, DateTime.Now, captured));
            // Wait 10 minutes
            // Thread.Sleep(1000 * 60 * 10);
        }

        public void TestAction()
        {

            // Wait 5 minutes
            // Thread.Sleep(1000 * 60 * 5);
        }

        public void TrainingAction()
        {
            // Wait 3 minutes
            Thread.Sleep(1000 * 60 * 3);
        }

        #endregion



    }
}
