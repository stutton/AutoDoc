using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stutton.AppExtensionClient.Contracts
{
    public interface IAppServiceRequestReceivedEventArgs
    {
        IAppServiceDeferral GetDeferral();
        IAppServiceRequest Reqeust { get; }
        Task<AppServiceResponseStatus> SendResponseAsync(IDictionary<string, object> returnMessage);
    }
}
