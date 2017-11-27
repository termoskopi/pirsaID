using Telkom.Pirsa.VPA.Api.Core.Blueprint.Services;

namespace Telkom.Pirsa.VPA.Api.Core.Blueprint
{
    public interface IApplicationServiceBuilder
    {
        /// <summary>
        /// Provides Seeder service methods access
        /// </summary>
        DatabaseSeedService SeederService { get; }

        /// <summary>
        /// Provides User management service methods access
        /// </summary>
        UserService UserService { get; }

        /// <summary>
        /// Provides Setting management service methods access
        /// </summary>
        SettingService SettingManagerService { get; }

        /// <summary>
        /// Provides Face Recognizer Service methods access
        /// </summary>
        RecognizerService FaceRecognizerService { get; }

        /// <summary>
        /// Provides Task Scheduler Service method access
        /// </summary>
        SchedulerService TaskSchedulerService { get; }

        /// <summary>
        /// Provides Activity Logger Service
        /// </summary>
        ActivityLogService LoggerService { get; }
    }
}
