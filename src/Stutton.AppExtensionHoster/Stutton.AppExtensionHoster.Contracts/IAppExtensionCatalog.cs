using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.AppExtensions;
using Windows.Foundation;

namespace Stutton.AppExtensionHoster.Contracts
{
    public interface IAppExtensionCatalog
    {
        event TypedEventHandler<IAppExtensionCatalog, IAppExtensionPackageInstalledEventArgs> PackageInstalled;
        event TypedEventHandler<IAppExtensionCatalog, IAppExtensionPackageUpdatedEventArgs> PackageUpdated;
        event TypedEventHandler<IAppExtensionCatalog, IAppExtensionPackageUninstallingEventArgs> PackageUninstalling;
        event TypedEventHandler<IAppExtensionCatalog, IAppExtensionPackageUpdatingEventArgs> PackageUpdating;
        event TypedEventHandler<IAppExtensionCatalog, IAppExtensionPackageStatusChangedEventArgs> PackageStatusChanged;

        void Open(string contractName);
        Task<IReadOnlyList<IAppExtension>> FindAllAsync();
        Task RequestRemovePackageAsync(string extensionFullName);
    }
}
