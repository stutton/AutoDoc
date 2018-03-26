using Windows.ApplicationModel.AppExtensions;
using Stutton.AppExtensionHoster.Contracts;

namespace Stutton.AppExtensionHoster.ContractImplementations
{
    public class AppExtensionPackageStatusChangedEventArgsWrapper : IAppExtensionPackageStatusChangedEventArgs
    {
        private readonly AppExtensionPackageStatusChangedEventArgs _args;

        public AppExtensionPackageStatusChangedEventArgsWrapper(AppExtensionPackageStatusChangedEventArgs args)
        {
            _args = args;
        }

        private IAppPackage _package;
        public IAppPackage Package => _package ?? (_package = new AppPackageWrapper(_args.Package));
    }
}
