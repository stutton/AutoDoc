using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stutton.AppExtensionClient.Contracts
{
    public interface IAppServiceRequestReceivedEventArgs
    {
        IDeferral GetDeferral();
        IAppServiceRequest Reqeust { get; }
        Task<ResponseStatus> SendResponseAsync(object returnMessage);
    }
}
