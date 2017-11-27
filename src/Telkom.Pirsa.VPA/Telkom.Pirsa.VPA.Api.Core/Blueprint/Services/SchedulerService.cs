using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telkom.Pirsa.VPA.Api.Data.BusinessLogic;
using Telkom.Pirsa.VPA.Api.Data.BusinessModel;
using Telkom.Pirsa.VPA.Api.Data.Core;
using Telkom.Pirsa.VPA.Api.Core.Extension;
using System.Threading;

namespace Telkom.Pirsa.VPA.Api.Core.Blueprint.Services
{
    public class SchedulerService
    {
        private readonly DataAccessManager _manager;
        private readonly IRepository _systemTaskRepository;

        public SchedulerService()
        { 
        
        }

        public SchedulerService(DataAccessManager manager)
        {
            _manager = manager;
            _systemTaskRepository = new SystemTaskRepository(_manager.ConnectionManager);
        }

        public IList<IDataModel> GetTasks(bool waiting = false)
        {
            try
            {
                if(!waiting)
                    return _systemTaskRepository.Get();
                else
                {
                    var model = new SystemTask();
                    model.StatusText = JobStatus.Waiting.GetDescription();
                    IList<Metadata> filters = new List<Metadata>();
                    filters.Add(model.ColumnsMetadata.Where(x => x.Database == SystemTask.ColumnStatusText).First());
                    return _systemTaskRepository.Get(model, filters);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public int CreateTask(TaskType taskType, int taskId, string user, string video = null)
        {
            try
            {
                var activity = string.Empty;

                if(taskType == TaskType.Capture)
                {
                    activity = string.Format("{0} request to capture a video ({1})", user, video);
                }
                else if(taskType == TaskType.Training)
                {
                    activity = string.Format("{0} request to train recognized model from captured faces", user);
                }
                else if(taskType == TaskType.Recognize)
                {
                    activity = string.Format("{0} request to test a video ({1})", user, video);
                }
                else throw new Exception("Selected task not available!");

                var model = new SystemTask()
                {
                    Id = (int)DateTime.Now.Ticks,
                    Activity = activity,
                    Status = (int)JobStatus.Created,
                    StatusText = JobStatus.Created.GetDescription()
                };
                _systemTaskRepository.Create(model);
                return model.Id;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public bool EnqueueTask(int taskId)
        {
            try
            {
                return UpdateTaskStatus(taskId, JobStatus.Waiting);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ExecuteTask(int taskId)
        {
            try
            {
                return UpdateTaskStatus(taskId, JobStatus.Processing);
            }
            catch (Exception ex)
            {
                throw ex;    
            }
        }

        public bool FinishTask(int taskId)
        {
            try
            {
                return UpdateTaskStatus(taskId, JobStatus.Completed);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool UpdateTaskStatus(int taskId, JobStatus status)
        {
            try
            {
                var model = new SystemTask();
                model.Id = taskId;
                model.Status = (int)status;
                model.StatusText = status.GetDescription();
                if (status == JobStatus.Completed)
                    model.FinishedDate = DateTime.Now;
                else if (status == JobStatus.Processing)
                {
                    model.StartDate = DateTime.Now;
                }
                else
                {
                    model.QueuedDate = DateTime.Now;
                }

                IList<Metadata> filters = new List<Metadata>();
                filters.Add(model.ColumnsMetadata.Where(x => x.IsPrimary).First());

                return _systemTaskRepository.Update(model, filters);
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

    }
}
