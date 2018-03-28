using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stutton.AppExtensionClient.Contracts
{
    public interface IAppServiceRequest
    {
        IDictionary<string, object> Message { get; }
        Task<ResponseStatus> SendResponseAsync(IDictionary<string, object> response);
    }
}
