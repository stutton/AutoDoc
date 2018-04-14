using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Stutton.AppExtensionClient.Contracts;

namespace Stutton.AppExtensionClient.Uwp
{
    public class BackgroundTaskDeferralWrapper : IDeferral
    {
        private readonly BackgroundTaskDeferral _deferral;

        public BackgroundTaskDeferralWrapper(BackgroundTaskDeferral deferral)
        {
            _deferral = deferral;
        }

        public void Complete()
        {
            _deferral.Complete();
        }
    }
}
