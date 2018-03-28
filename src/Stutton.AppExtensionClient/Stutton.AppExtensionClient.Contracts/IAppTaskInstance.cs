using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stutton.AppExtensionClient.Contracts
{
    public interface IAppTaskInstance
    {
        event Action<object, CancellationReason> Canceled;
        IAppServiceTriggerDetails TriggerDetails { get; }
        IAppServiceDeferral GetDeferral();
    }
}
