using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telkom.Pirsa.VPA.Api.Core.Blueprint.Services
{
    public class ApplicationServiceBuilder : IApplicationServiceBuilder
    {
        private readonly DataAccessManager _dataAccess;
        private readonly DatabaseSeedService _seederService;
        private readonly UserService _userService;
        private readonly SettingService _settingService;
        private readonly RecognizerService _recognizerService;
        private readonly SchedulerService _schedulerService;
        private readonly ActivityLogService _loggerService;
        private readonly ITaskScheduler _scheduler;

        public ApplicationServiceBuilder()
        {
            _dataAccess = new DataAccessManager();
            _seederService = new DatabaseSeedService(_dataAccess);
            _loggerService = new ActivityLogService(_dataAccess);
            _userService = new UserService(_dataAccess);
            _settingService = new SettingService(_dataAccess);
            _schedulerService = new SchedulerService(_dataAccess);
            _scheduler = new RecognizerTaskScheduler(_schedulerService, _loggerService);
            _recognizerService = new RecognizerService(_dataAccess, _scheduler, _schedulerService, _loggerService);
        }

        public DatabaseSeedService SeederService
        {
            get { return _seederService; }
        }

        public UserService UserService
        {
            get { return _userService; }
        }

        public SettingService SettingManagerService
        {
            get { return _settingService; }
        }

        public RecognizerService FaceRecognizerService
        {
            get { return _recognizerService; }
        }

        public SchedulerService TaskSchedulerService
        {
            get { return _schedulerService; }
        }

        public ActivityLogService LoggerService
        {
            get { return _loggerService; }
        }
    }
}
