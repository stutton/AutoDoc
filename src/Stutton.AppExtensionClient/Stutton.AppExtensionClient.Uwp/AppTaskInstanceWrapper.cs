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
    public class AppTaskInstanceWrapper : IAppTaskInstance
    {
        private readonly IBackgroundTaskInstance _task;

        public AppTaskInstanceWrapper(IBackgroundTaskInstance task)
        {
            _task = task;
            task.Canceled += (s, e) => Canceled?.Invoke(s, e.ConvertCancellationReason());
        }

        public event Action<object, CancellationReason> Canceled;

        public IAppServiceTriggerDetails TriggerDetails =>
            new AppServiceTriggerDetailsWrapper(_task.TriggerDetails as AppServiceTriggerDetails);

        public IDeferral GetDeferral()
        {
            return new BackgroundTaskDeferralWrapper(_task.GetDeferral());
        }
    }
}
