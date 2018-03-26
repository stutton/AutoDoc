using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stutton.AppExtensionHoster.Contracts
{
    public interface IDispatcher
    {
        Task RunAsync(DispatcherPriority priority, Action handler);
    }
}
