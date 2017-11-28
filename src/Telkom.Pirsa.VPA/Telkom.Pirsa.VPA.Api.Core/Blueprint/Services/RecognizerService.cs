using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telkom.Pirsa.VPA.Api.Data.BusinessLogic;
using Telkom.Pirsa.VPA.Api.Data.BusinessModel;
using Telkom.Pirsa.VPA.Api.Data.Core;
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

        private readonly IRepository _systemTaskRepository;
        private readonly IRepository _resultRepository;
        private readonly IRepository _resultItemRepository;

        public RecognizerService()
        {
            throw new Exception("This default constructor has been disbaled. All Service instance only generated on ApplicationServiceBuilder");
        }

        public RecognizerService(DataAccessManager manager, ITaskScheduler scheduler, SchedulerService service, ActivityLogService logger)
        {
            _manager = manager;
            _scheduler = scheduler;
            _service = service;
            _logger = logger;

            _systemTaskRepository = new SystemTaskRepository(_manager.ConnectionManager);
            _resultRepository = new ResultRepository(_manager.ConnectionManager);
            _resultItemRepository = new ResultItemRepository(_manager.ConnectionManager);

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
                long taskId = _service.CreateTask(TaskType.Capture, (int)task.ID, user, video);
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
                long taskId = _service.CreateTask(TaskType.Training, task.ID, user);
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
                task.SetAction(() => TestAction(video));
                _scheduler.EnqueueJob(task);
                long taskId = _service.CreateTask(TaskType.Recognize, task.ID, user, video);
                return _service.EnqueueTask(taskId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JArray GetImageResults()
        {
            try
            {
                _setting = SettingManager.ReadFromFile();
                string[] files;
                _recognizer.LoadTestData(out files);
                var resultArray = new JArray();

                if (files != null)
                {
                    foreach (var file in files)
                    {
                        resultArray.Add(file);
                    }
                }

                return resultArray;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JArray GetSchedules()
        {
            try
            {
               var result =  _systemTaskRepository.Get();
               var resultArray = new JArray();
               if (result != null)
               {
                   foreach (var item in result)
                   {
                       resultArray.Add(JObject.Parse(item.Json));
                   }
               }
               return resultArray;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JArray GetResults()
        {
            try
            {
                var resultArray = new JArray();

                var results = _resultRepository.Get();
                if (results != null)
                {
                    foreach (var item in results)
                    { 
                        var model = new ResultItem();
                        model.ResultId = ((Result)item).Id;
                        IList<Metadata> filter = new List<Metadata>();
                        filter.Add(model.ColumnsMetadata.Where(x => x.Database == ResultItem.ColumnResultId).First());
                        var resultItems = _resultItemRepository.Get(model, filter);
                        var resultObj = JObject.Parse(item.Json);
                        var resultItemArray = new JArray();
                        if (resultItems != null)
                        {
                            foreach (var itt in resultItems)
                            {
                                resultItemArray.Add(JObject.Parse(itt.Json));
                            }
                        }
                        resultObj.Add(new JProperty("Items", resultItemArray));
                        resultArray.Add(resultObj);
                    }
                }
                return resultArray;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Dummy Scheculer Actions
        public void CaptureAction(string path, string name)
        {
          _logger.LogSystemActivity(string.Format("Starting capture video {0} with name {1} at {2:dd MMMM yyyy HH:mm}", path, name, DateTime.Now));
          //_setting = SettingManager.ReadFromFile();
          //var captured = VideoCapture.Capture(path, name, _setting, VideoCapture.CaptureOptions.CaptureAndSave, VideoCapture.CaptureSaveOption.SaveOriginalResized);
          // Wait 10 minutes
          Thread.Sleep(1000 * 60 * 10);
          _logger.LogSystemActivity(string.Format("Finished capture video {0} with name {1} at {2:dd MMMM yyyy HH:mm}: {3} frames captured", path, name, DateTime.Now, 1000));
            
        }

        public void TestAction(string path)
        {
            _logger.LogSystemActivity(string.Format("Starting test video {0} at {1:dd MMMM yyyy HH:mm}", path,  DateTime.Now));
            // Wait 5 minutes
            Thread.Sleep(1000 * 60 * 5);
            _logger.LogSystemActivity(string.Format("Completed test video {0} at {1:dd MMMM yyyy HH:mm}", path, DateTime.Now));
        }

        public void TrainingAction()
        {
            _logger.LogSystemActivity(string.Format("Starting train model from captured images at {0:dd MMMM yyyy HH:mm}", DateTime.Now));
            // Wait 3 minutes
            Thread.Sleep(1000 * 60 * 3);
            _logger.LogSystemActivity(string.Format("Completed train model from captured images at {0:dd MMMM yyyy HH:mm}", DateTime.Now));
        }

        #endregion



    }
}
