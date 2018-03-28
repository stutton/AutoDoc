using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stutton.AppExtensionClient.Contracts
{
    public interface IAppServiceConnection
    {
        event EventHandler<IAppServiceRequestReceivedEventArgs> RequestReceived;
        event EventHandler<IAppServiceClosedEventArgs> ServiceClosed;
    }
}
