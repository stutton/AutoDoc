using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stutton.AppExtensionHoster.Contracts
{
    public enum AppConnectionStatus
    {
        AppNotInstalled,
        AppServiceUnavailable,
        AppUnavailable,
        NotAuthorized,
        RemoteSystemUnavailable,
        RemoteSystemNotSupportedByApp,
        Success,
        Unknown
    }
}
