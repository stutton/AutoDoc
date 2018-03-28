using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stutton.AppExtensionClient.Contracts
{
    public enum CancellationReason
    {
        Abort,
        Terminating,
        LoggingOff,
        ServicingUpdate,
        IdleTask,
        Uninstall,
        ConditionLoss,
        SystemPolicy,
        ExecutionTimeExceeded,
        ResourceRevocation,
        EnergySaver
    }
}
