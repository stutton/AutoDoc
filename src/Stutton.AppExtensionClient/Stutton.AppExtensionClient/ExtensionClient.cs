using System;
using System.Collections;
using System.Collections.Generic;
using Stutton.AppExtensionClient.Contracts;

namespace Stutton.AppExtensionClient
{
    public abstract class ExtensionClient<TMessage, TResponse>
    {
        private IAppServiceDeferral _deferral;
        private IAppServiceConnection _connection;

        public void Initialize(IAppTaskInstance task)
        {
            task.Canceled += Task_Canceled;
            _deferral = task.GetDeferral();

            var triggerDetails = task.TriggerDetails;
            _connection = triggerDetails.AppServiceConnection;

            _connection.RequestReceived += Connection_RequestReceived;
            _connection.ServiceClosed += Connection_ServiceClosed;
        }

        protected abstract TResponse Run(TMessage message);

        private async void Connection_RequestReceived(object sender, IAppServiceRequestReceivedEventArgs args)
        {
            var requestDeferral = args.GetDeferral();
            var messageSet = args.Reqeust.Message;
            var returnMessage = new Dictionary<string, object>();

            if (!messageSet.ContainsKey("message"))
            {
                returnMessage.Add("error", new ExtensionClientException($"Request message did not contain a key 'message'"));
                await args.SendResponseAsync(returnMessage);
                requestDeferral.Complete();
            }

            var messageObj = messageSet["message"];
            if (!(messageObj is TMessage))
            {
                returnMessage.Add("error", new ExtensionClientException($"Message was not of the correct type. Expected '{typeof(TMessage).Name}' but received '{messageObj.GetType().Name}'"));
                await args.SendResponseAsync(returnMessage);
                requestDeferral.Complete();
            }

            var message = (TMessage) messageObj;
            

            var response = Run(message);

            returnMessage.Add("result", response);
            var result = await args.SendResponseAsync(returnMessage);
            // TODO: Log result
            requestDeferral.Complete();
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
