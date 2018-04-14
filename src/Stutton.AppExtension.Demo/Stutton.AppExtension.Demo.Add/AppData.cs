using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stutton.AppExtension.Demo.Shared;
using Stutton.AppExtensionClient;

namespace Stutton.AppExtension.Demo.Add
{
    public static class AppData
    {
        public static ExtensionClient<AppExtensionMessage,AppExtensionResponse> ExtensionClient { get; set; } = new AddExtensionClient();
    }
}
