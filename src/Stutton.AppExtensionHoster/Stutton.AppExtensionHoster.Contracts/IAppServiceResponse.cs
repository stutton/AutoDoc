using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.AppService;
using Windows.Foundation.Collections;

namespace Stutton.AppExtensionHoster.Contracts
{
    public interface IAppServiceResponse
    {
        ValueSet Message { get; }
        AppServiceResponseStatus Status { get; }
    }
}
