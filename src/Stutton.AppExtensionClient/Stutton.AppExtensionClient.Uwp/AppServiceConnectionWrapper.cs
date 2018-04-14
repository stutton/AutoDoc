using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.AppService;
using Stutton.AppExtensionClient.Contracts;

namespace Stutton.AppExtensionClient.Uwp
{
    public class AppServiceConnectionWrapper : IAppServiceConnection
    {
        public AppServiceConnectionWrapper(AppServiceConnection connection)
        {
            connection.RequestReceived += (s, e) =>
                RequestReceived?.Invoke(this, new AppServiceRequestReceivedEventArgsWrapper(e));

            connection.ServiceClosed += (s, e) => ServiceClosed?.Invoke(this, new AppServiceClosedEventArgsWrapper(e));
        }

        public event EventHandler<IAppServiceRequestReceivedEventArgs> RequestReceived;
        public event EventHandler<IAppServiceClosedEventArgs> ServiceClosed;
    }
}
