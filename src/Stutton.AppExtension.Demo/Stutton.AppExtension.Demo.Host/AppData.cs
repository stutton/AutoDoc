using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stutton.AppExtension.Demo.Shared;
using Stutton.AppExtensionHoster;
using Stutton.AppExtensionHoster.ContractImplementations;
using Stutton.AppExtensionHoster.Contracts;
using Stutton.AppExtensionHoster.Uwp;

namespace Stutton.AppExtension.Demo.Host
{
    public static class AppData
    {
        public static ExtensionManager<AppExtensionMessage, AppExtensionResponse> ExtensionManager { get; set; } =
            new ExtensionManager<AppExtensionMessage, AppExtensionResponse>(
                "Stutton.AppExtension.Demo.Extension",
                new[] {SignatureKind.None, SignatureKind.Store, SignatureKind.Developer, SignatureKind.System},
                new AppExtensionCatalogWrapper(),
                new AppServiceConnectionFactor());
    }
}
