using System;
using System.ComponentModel;

namespace Telkom.Pirsa.VPA.Api.Core.Blueprint
{
    public enum JobStatus : int
    {
        [Description("Invalid")]
        Invalid = 0,

        [Description("Created")]
        Created = 1,

        [Description("Waiting")]
        Waiting = 2,

        [Description("Processing")]
        Processing = 3,

        [Description("Completed")]
        Completed = 4,

        [Description("Aborted")]
        Aborted = 5

    }

    public enum TaskType : int
    { 
        [Description("NotAvailable")]
        None = 0,

        [Description("Capturing")]
        Capture,

        [Description("Training")]
        Training,

        [Description("Recognize")]
        Recognize
    }
}
