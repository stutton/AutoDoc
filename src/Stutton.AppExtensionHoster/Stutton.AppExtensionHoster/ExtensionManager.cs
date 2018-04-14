using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stutton.AppExtensionHoster.Contracts;
using System.Runtime.CompilerServices;

[assembly:InternalsVisibleTo("Stutton.AppExtensionHoster.Tests")]

namespace Stutton.AppExtensionHoster
{
    public class ExtensionManager<TMessage, TResponse>
    {
        private readonly IEnumerable<SignatureKind> _allowedPackageSignatureKinds;
        private readonly IAppExtensionCatalog _catalog;
        private readonly IAppServiceConnectionFactory _connectionFactory;
        private IDispatcher _dispatcher;

        /// <summary>
        /// Creates a new ExtensionManager that will monitor and notify changes to extensions using the specified contract name.
        /// Note: The ExtensionManager must also be initialized before use.
        /// </summary>
        /// <param name="contractName">The name used to monitor the extension catalog. Only extensions that specify the same name will be found</param>
        /// <param name="allowedPackageSignatureKinds">List of accepted package signature kinds that extension apps must have in order to be loaded</param>
        public ExtensionManager(string contractName, IEnumerable<SignatureKind> allowedPackageSignatureKinds, IAppExtensionCatalog catalog, IAppServiceConnectionFactory connectionFactory)
        {
            _allowedPackageSignatureKinds = allowedPackageSignatureKinds;
            ContractName = contractName;
            Extensions = new ObservableCollection<Extension<TMessage, TResponse>>();
            _catalog = catalog;
            _connectionFactory = connectionFactory;
            _catalog.Open(contractName);
            _dispatcher = null;
        }

        /// <summary>
        /// List of all available extensions using the contract name provided
        /// </summary>
        public ObservableCollection<Extension<TMessage, TResponse>> Extensions { get; }

        public string ContractName { get; }

        /// <summary>
        /// Initilializes a the ExtensionManager and loads all available plugins. 
        /// Because the ExtensionManager sends notification about plugin status changes that could be bound to a UI it requires a CoreDispatcher
        /// which is used to update the Extensions collection.
        /// </summary>
        /// <param name="dispatcher"></param>
        /// <returns></returns>
        public void InitializeAsync(IDispatcher dispatcher)
        {
            #region Error Handling
            if (_dispatcher != null)
            {
                throw new ExtensionManagerException($"ExtensionManager for {ContractName} is already initialized");
            }
            #endregion

            _dispatcher = dispatcher;

            _catalog.PackageInstalled += Catalog_PackageInstalled;
            _catalog.PackageUpdated += Catalog_PackageUpdated;
            _catalog.PackageUninstalling += Catalog_PackageUninstalling;
            _catalog.PackageUpdating += Catalog_PackageUpdating;
            _catalog.PackageStatusChanged += Catalog_PackageStatusChanged;

            FindAndLoadExtensions();
        }

        public async void RemoveExtension(Extension<TMessage, TResponse> extension)
        {
            await _catalog.RequestRemovePackageAsync(extension.AppExtension.Package.FullName);
        }

        private async Task LoadExtension(IAppExtension extension)
        {
            if (!extension.Package.VerifyIsOK() || !_allowedPackageSignatureKinds.Contains(extension.Package.SignatureKind))
            {
                //TODO: Log
                return;
            }

            var identifier = extension.GetUniqueId();

            var existingExtension = Extensions.FirstOrDefault(e => e.UniqueId == identifier);

            if (existingExtension == null)
            {
                var newExtension = new Extension<TMessage, TResponse>(extension, _connectionFactory);
                Extensions.Add(newExtension);
                await newExtension.Initialize();
                newExtension.Load();
            }
            else
            {
                existingExtension.Unload();
                await existingExtension.Update(extension);
            }
        }

        private async void FindAndLoadExtensions()
        {
            var extensions = await _catalog.FindAllAsync();
            foreach (var extension in extensions)
            {
                await LoadExtension(extension);
            }
        }

        private async Task LoadExtensions(IAppPackage package)
        {
            await _dispatcher.RunAsync(DispatcherPriority.Normal,
                () =>
                {
                    Extensions.Where(e => e.AppExtension.Package.FamilyName == package.FamilyName).ToList()
                        .ForEach(ext => ext.Load());
                });
        }

        private async Task UnloadExtensions(IAppPackage package)
        {
            await _dispatcher.RunAsync(DispatcherPriority.Normal,
                () =>
                {
                    Extensions.Where(e => e.AppExtension.Package.FamilyName == package.FamilyName).ToList()
                        .ForEach(ext => ext.Unload());
                });
        }

        private async void Catalog_PackageInstalled(object sender, IAppExtensionPackageInstalledEventArgs args)
        {
            await _dispatcher.RunAsync(DispatcherPriority.Normal, async () =>
            {
                foreach (var extension in args.Extensions)
                {
                    await LoadExtension(extension);
                }
            });
        }

        private async Task RemoveExtensions(IAppPackage package)
        {
            await _dispatcher.RunAsync(DispatcherPriority.Normal,
                () =>
                {
                    Extensions.Where(e => e.AppExtension.Package.FamilyName == package.FamilyName).ToList()
                        .ForEach(ext =>
                        {
                            ext.Unload();
                            Extensions.Remove(ext);
                        });
                });
        }

        private async void Catalog_PackageUpdated(object sender, IAppExtensionPackageUpdatedEventArgs args)
        {
            await _dispatcher.RunAsync(DispatcherPriority.Normal, async () =>
            {
                foreach (var extension in args.Extensions)
                {
                    await LoadExtension(extension);
                }
            });
        }

        private async void Catalog_PackageUninstalling(object sender, IAppExtensionPackageUninstallingEventArgs args)
        {
            await RemoveExtensions(args.Package);
        }

        private async void Catalog_PackageUpdating(object sender, IAppExtensionPackageUpdatingEventArgs args)
        {
            await UnloadExtensions(args.Package);
        }

        private async void Catalog_PackageStatusChanged(object sender, IAppExtensionPackageStatusChangedEventArgs args)
        {
            if (!args.Package.VerifyIsOK())
            {
                if (args.Package.Offline)
                {
                    await UnloadExtensions(args.Package);
                }
                else if (args.Package.Servicing || args.Package.DeploymentInProgress)
                {
                    // Do nothing
                }
                else
                {
                    await RemoveExtensions(args.Package);
                }
            }
            else
            {
                await LoadExtensions(args.Package);
            }
        }
    }
}
