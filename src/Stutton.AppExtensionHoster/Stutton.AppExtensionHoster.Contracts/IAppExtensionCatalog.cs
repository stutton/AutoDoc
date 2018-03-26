using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stutton.AppExtensionHoster.Contracts
{
    public interface IAppExtensionCatalog
    {
        event EventHandler<IAppExtensionPackageInstalledEventArgs> PackageInstalled;
        event EventHandler<IAppExtensionPackageUpdatedEventArgs> PackageUpdated;
        event EventHandler<IAppExtensionPackageUninstallingEventArgs> PackageUninstalling;
        event EventHandler<IAppExtensionPackageUpdatingEventArgs> PackageUpdating;
        event EventHandler<IAppExtensionPackageStatusChangedEventArgs> PackageStatusChanged;

        void Open(string contractName);
        Task<IReadOnlyList<IAppExtension>> FindAllAsync();
        Task RequestRemovePackageAsync(string extensionFullName);
    }
}
