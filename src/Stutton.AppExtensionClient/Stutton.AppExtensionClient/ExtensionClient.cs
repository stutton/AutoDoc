using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Stutton.AppExtensionClient.Contracts;

namespace Stutton.AppExtensionClient
{
    public abstract class ExtensionClient<TMessage, TResponse>
    {
        private IDeferral _deferral;
        private IAppServiceConnection _connection;
        private bool _initialized = false;

        public void Initialize(IAppTaskInstance task)
        {
            if (_initialized)
                return;

            task.Canceled += Task_Canceled;

            var triggerDetails = task.TriggerDetails;

            _deferral = task.GetDeferral();
            
            _connection = triggerDetails.AppServiceConnection;

            _connection.RequestReceived += Connection_RequestReceived;
            _connection.ServiceClosed += Connection_ServiceClosed;

            _initialized = true;
        }

        protected abstract Task<TResponse> Run(TMessage message);

        private async void Connection_RequestReceived(object sender, IAppServiceRequestReceivedEventArgs args)
        {
            var requestDeferral = args.GetDeferral();
            try
            {
                var messageSet = args.Reqeust.Message;
                var returnMessage = new Dictionary<string, object>();

                if (!messageSet.ContainsKey("message"))
                {
                    returnMessage.Add("error",
                        new ExtensionClientException($"Request message did not contain a key 'message'"));
                    await args.SendResponseAsync(returnMessage);
                    return;
                }

                var jsonMessage = messageSet["message"] as string;
                var message = JsonConvert.DeserializeObject<TMessage>(jsonMessage);
                if (message == null)
                {
                    returnMessage.Add("error",
                        new ExtensionClientException(
                            $"Unable to deserialize message to type '{typeof(TMessage)}'. Message: '{jsonMessage}'"));
                    await args.SendResponseAsync(returnMessage);
                    return;
                }

                var response = await Run(message);
                var result = await args.SendResponseAsync(response);
                // TODO: Log result
            }
            finally
            {
                requestDeferral.Complete();
            }
        }

        private void Connection_ServiceClosed(object sender, IAppServiceClosedEventArgs args)
        {
            _deferral.Complete();
        }

        private void Task_Canceled(object sender, CancellationReason reason)
        {
            _deferral.Complete();
        }
    }
}
