using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stutton.AppExtension.Demo.Shared;
using Stutton.AppExtensionClient;

namespace Stutton.AppExtension.Demo.Add
{
    public class AddExtensionClient : ExtensionClient<AppExtensionMessage, AppExtensionResponse>
    {
        protected override Task<AppExtensionResponse> Run(AppExtensionMessage message)
        {
            var result = new AppExtensionResponse {Result = message.Parameters.Sum()};
            return Task.FromResult(result);
        }
    }
}
