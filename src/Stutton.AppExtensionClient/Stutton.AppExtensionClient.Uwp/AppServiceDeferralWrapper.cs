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
    public class AppServiceDeferralWrapper : IDeferral
    {
        private readonly AppServiceDeferral _deferral;

        public AppServiceDeferralWrapper(AppServiceDeferral deferral)
        {
            _deferral = deferral;
        }

        public void Complete()
        {
            _deferral.Complete();
        }
    }
}
