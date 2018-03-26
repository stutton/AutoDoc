using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stutton.AppExtensionHoster.Contracts
{
    public interface IAppServiceConnection : IDisposable
    {
        string AppServiceName { get; set; }
        string PackageFamilyName { get; set; }

        Task<AppConnectionStatus> OpenAsync();
        Task<IAppServiceResponse> SendMessageAsync(object message);
    }
}
