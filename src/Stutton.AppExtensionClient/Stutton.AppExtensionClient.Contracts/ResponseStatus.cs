using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stutton.AppExtensionClient.Contracts
{
    public enum ResponseStatus
    {
        Success,
        Failure,
        ResourceLimitsExceeded,
        Unknown,
        RemoteSystemUnavailable,
        MessageSizeTooLarge
    }
}
