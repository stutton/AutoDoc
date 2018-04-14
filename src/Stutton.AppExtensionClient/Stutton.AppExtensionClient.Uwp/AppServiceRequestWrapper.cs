using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.AppService;
using Windows.Foundation.Collections;
using Newtonsoft.Json;
using Stutton.AppExtensionClient.Contracts;

namespace Stutton.AppExtensionClient.Uwp
{
    public class AppServiceRequestWrapper : IAppServiceRequest
    {
        private readonly AppServiceRequest _request;

        public AppServiceRequestWrapper(AppServiceRequest request)
        {
            _request = request;
        }

        public IDictionary<string, object> Message => _request.Message;

        public async Task<ResponseStatus> SendResponseAsync(object response)
        {
            var jsonMessage = JsonConvert.SerializeObject(response);
            var responseSet = new ValueSet {{"result", jsonMessage}};
            return (await _request.SendResponseAsync(responseSet)).ConvertResponseStatus();
        }
    }
}
