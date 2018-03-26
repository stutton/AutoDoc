using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.AppExtensions;
using Stutton.AppExtensionHoster.Contracts;

namespace Stutton.AppExtensionHoster.ContractImplementations
{
    public class AppExtensionPackageInstalledEventArgsWrapper : IAppExtensionPackageInstalledEventArgs
    {
        private readonly AppExtensionPackageInstalledEventArgs _args;

        public AppExtensionPackageInstalledEventArgsWrapper(AppExtensionPackageInstalledEventArgs args)
        {
            _args = args;
        }

        public IReadOnlyList<IAppExtension> Extensions =>
            _args.Extensions.Select(e => new AppExtensionWrapper(e)).ToList();
    }
}
