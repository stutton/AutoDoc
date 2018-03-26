using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;
using Stutton.AppExtensionHoster.ContractImplementations;
using Stutton.AppExtensionHoster.Contracts;

namespace Stutton.AppExtensionHoster.Uwp
{
    public class CoreDispatcherWrapper : IDispatcher
    {
        private readonly CoreDispatcher _dispatcher;

        public CoreDispatcherWrapper(CoreDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        public async Task RunAsync(DispatcherPriority priority, Action handler)
        {
            await _dispatcher.RunAsync(priority.GetCoreDispatcherPriority(), () => handler());
        }
    }
}
