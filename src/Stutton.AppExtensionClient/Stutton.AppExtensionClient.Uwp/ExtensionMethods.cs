using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.AppService;
using Windows.ApplicationModel.Background;
using Stutton.AppExtensionClient.Contracts;

namespace Stutton.AppExtensionClient.Uwp
{
    internal static class ExtensionMethods
    {
        public static ResponseStatus ConvertResponseStatus(this AppServiceResponseStatus status)
        {
            switch (status)
            {
                case AppServiceResponseStatus.Success:
                    return ResponseStatus.Success;
                case AppServiceResponseStatus.Failure:
                    return ResponseStatus.Failure;
                case AppServiceResponseStatus.ResourceLimitsExceeded:
                    return ResponseStatus.ResourceLimitsExceeded;
                case AppServiceResponseStatus.Unknown:
                    return ResponseStatus.Unknown;
                case AppServiceResponseStatus.RemoteSystemUnavailable:
                    return ResponseStatus.RemoteSystemUnavailable;
                case AppServiceResponseStatus.MessageSizeTooLarge:
                    return ResponseStatus.MessageSizeTooLarge;
                default:
                    throw new ArgumentOutOfRangeException(nameof(status), status, null);
            }
        }

        public static CancellationReason ConvertCancellationReason(this BackgroundTaskCancellationReason reason)
        {
            switch (reason)
            {
                case BackgroundTaskCancellationReason.Abort:
                    return CancellationReason.Abort;
                case BackgroundTaskCancellationReason.Terminating:
                    return CancellationReason.Terminating;
                case BackgroundTaskCancellationReason.LoggingOff:
                    return CancellationReason.LoggingOff;
                case BackgroundTaskCancellationReason.ServicingUpdate:
                    return CancellationReason.ServicingUpdate;
                case BackgroundTaskCancellationReason.IdleTask:
                    return CancellationReason.IdleTask;
                case BackgroundTaskCancellationReason.Uninstall:
                    return CancellationReason.Uninstall;
                case BackgroundTaskCancellationReason.ConditionLoss:
                    return CancellationReason.ConditionLoss;
                case BackgroundTaskCancellationReason.SystemPolicy:
                    return CancellationReason.SystemPolicy;
                case BackgroundTaskCancellationReason.ExecutionTimeExceeded:
                    return CancellationReason.ExecutionTimeExceeded;
                case BackgroundTaskCancellationReason.ResourceRevocation:
                    return CancellationReason.ResourceRevocation;
                case BackgroundTaskCancellationReason.EnergySaver:
                    return CancellationReason.EnergySaver;
                default:
                    throw new ArgumentOutOfRangeException(nameof(reason), reason, null);
            }
        }
    }
}
