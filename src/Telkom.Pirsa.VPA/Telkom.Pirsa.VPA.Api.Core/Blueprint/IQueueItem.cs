using System;
namespace Telkom.Pirsa.VPA.Api.Core.Blueprint
{
    public interface IQueueItem
    {
        JobStatus Status { get; }
        long ID { get; }
        string Title { get; }
        string Description { get; }
        string StatusDescription { get; }
        void UpdateJob(JobStatus status);
        void SetAction(Action action);
        bool HasAction { get; }
        bool IsCanceled { get; }
        void Execute();
    }
}
