using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.AppExtensions;
using Stutton.AppExtensionHoster.Contracts;

namespace Stutton.AppExtensionHoster.ContractImplementations
{
    public class AppExtensionPackageUninstallingEventArgsWrapper : IAppExtensionPackageUninstallingEventArgs
    {
        private readonly AppExtensionPackageUninstallingEventArgs _args;

        public AppExtensionPackageUninstallingEventArgsWrapper(AppExtensionPackageUninstallingEventArgs args)
        {
            _args = args;
        }

        private IAppPackage _package;
        public IAppPackage Package => _package ?? (_package = new AppPackageWrapper(_args.Package));
    }
}
