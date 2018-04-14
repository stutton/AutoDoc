using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.AppService;
using Stutton.AppExtensionClient.Contracts;

namespace Stutton.AppExtensionClient.Uwp
{
    public class AppServiceClosedEventArgsWrapper : IAppServiceClosedEventArgs
    {
        public AppServiceClosedEventArgsWrapper(AppServiceClosedEventArgs args)
        {
            EventArgs = args;
        }

        public AppServiceClosedEventArgs EventArgs { get; }
    }
}
