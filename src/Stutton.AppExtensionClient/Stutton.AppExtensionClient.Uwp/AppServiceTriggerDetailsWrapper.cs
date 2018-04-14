using Windows.ApplicationModel.AppService;
using Stutton.AppExtensionClient.Contracts;

namespace Stutton.AppExtensionClient.Uwp
{
    internal class AppServiceTriggerDetailsWrapper : IAppServiceTriggerDetails
    {
        private AppServiceTriggerDetails _triggerDetails;

        public AppServiceTriggerDetailsWrapper(AppServiceTriggerDetails triggerDetails)
        {
            _triggerDetails = triggerDetails;
        }

        public IAppServiceConnection AppServiceConnection => new AppServiceConnectionWrapper(_triggerDetails.AppServiceConnection);
    }
}