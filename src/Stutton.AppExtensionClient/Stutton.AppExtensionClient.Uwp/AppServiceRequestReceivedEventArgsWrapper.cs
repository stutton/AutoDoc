using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.AppService;
using Windows.Foundation.Collections;
using Stutton.AppExtensionClient.Contracts;

namespace Stutton.AppExtensionClient.Uwp
{
    public class AppServiceRequestReceivedEventArgsWrapper : IAppServiceRequestReceivedEventArgs
    {
        private readonly AppServiceRequestReceivedEventArgs _args;

        public AppServiceRequestReceivedEventArgsWrapper(AppServiceRequestReceivedEventArgs args)
        {
            _args = args;
        }

        public IDeferral GetDeferral()
        {
            return new AppServiceDeferralWrapper(_args.GetDeferral());
        }

        public IAppServiceRequest Reqeust => new AppServiceRequestWrapper(_args.Request);

        public async Task<ResponseStatus> SendResponseAsync(object returnMessage)
        {
            
            return await Reqeust.SendResponseAsync(returnMessage);
        }
    }
}
