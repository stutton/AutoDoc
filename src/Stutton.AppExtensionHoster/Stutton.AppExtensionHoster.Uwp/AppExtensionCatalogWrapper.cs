using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.AppExtensions;
using Windows.Foundation;
using Stutton.AppExtensionHoster.Contracts;

namespace Stutton.AppExtensionHoster.ContractImplementations
{
    public class AppExtensionCatalogWrapper : IAppExtensionCatalog
    {
        private AppExtensionCatalog _catalog;

        public event EventHandler<IAppExtensionPackageInstalledEventArgs> PackageInstalled;
        public event EventHandler<IAppExtensionPackageUpdatedEventArgs> PackageUpdated;
        public event EventHandler<IAppExtensionPackageUninstallingEventArgs> PackageUninstalling;
        public event EventHandler<IAppExtensionPackageUpdatingEventArgs> PackageUpdating;
        public event EventHandler<IAppExtensionPackageStatusChangedEventArgs> PackageStatusChanged;

        public void Open(string contractName)
        {
            _catalog = AppExtensionCatalog.Open(contractName);

            _catalog.PackageInstalled += (s, e) => PackageInstalled?.Invoke(this, new AppExtensionPackageInstalledEventArgsWrapper(e));
            _catalog.PackageUpdated += (s, e) => PackageUpdated?.Invoke(this, new AppExtensionPackageUpdatedEventArgsWrapper(e));
            _catalog.PackageUninstalling += (s, e) => PackageUninstalling?.Invoke(this, new AppExtensionPackageUninstallingEventArgsWrapper(e));
            _catalog.PackageUpdating += (s, e) => PackageUpdating?.Invoke(this, new AppExtensionPackageUpdatingEventArgsWrapper(e));
            _catalog.PackageStatusChanged += (s, e) => PackageStatusChanged?.Invoke(this, new AppExtensionPackageStatusChangedEventArgsWrapper(e));
        }

        public async Task<IReadOnlyList<IAppExtension>> FindAllAsync()
        {
            var extensions = await _catalog.FindAllAsync();
            return extensions.Select(e => new AppExtensionWrapper(e)).ToList();
        }

        public async Task RequestRemovePackageAsync(string extensionFullName)
        {
            await _catalog.RequestRemovePackageAsync(extensionFullName);
        }
    }
}
