using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.AppService;

namespace Stutton.AppExtensionHoster.Contracts
{
    public interface IAppServiceConnection
    {
        string AppServiceName { get; set; }
        string PackageFamilyName { get; set; }

        Task<AppServiceConnectionStatus> OpanAsync();
        Task<IAppServiceResponse> SendMessageAsync();
    }
}
