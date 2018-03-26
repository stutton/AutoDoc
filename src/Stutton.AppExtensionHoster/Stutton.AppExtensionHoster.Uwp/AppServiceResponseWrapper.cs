using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stutton.AppExtensionHoster.Contracts;

namespace Stutton.AppExtensionHoster.Uwp
{
    public class AppServiceResponseWrapper : IAppServiceResponse
    {
        public AppServiceResponseWrapper(IDictionary<string, object> message, AppResponseStatus status)
        {
            Message = message;
            Status = status;
        }

        public IDictionary<string, object> Message { get; }
        public AppResponseStatus Status { get; }
    }
}
