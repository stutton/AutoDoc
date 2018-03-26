using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stutton.AppExtensionHoster.Contracts
{
    public enum DispatcherPriority
    {
        Idle = -2,
        Low = -1,
        Normal = 0,
        High = 1
    }
}
