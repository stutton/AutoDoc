﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.AppService;
using Windows.Foundation.Collections;
using Newtonsoft.Json;
using Stutton.AppExtensionHoster.ContractImplementations;
using Stutton.AppExtensionHoster.Contracts;

namespace Stutton.AppExtensionHoster.Uwp
{
    public class AppServiceConnectionWrapper : IAppServiceConnection
    {
        private AppServiceConnection _connection;

        public AppServiceConnectionWrapper()
        {
            _connection = new AppServiceConnection();
        }

        public string AppServiceName { get => _connection.AppServiceName; set => _connection.AppServiceName = value; }
        public string PackageFamilyName { get => _connection.PackageFamilyName; set => _connection.PackageFamilyName = value; }

        public async Task<AppConnectionStatus> OpenAsync()
        {
            var status = await _connection.OpenAsync();
            return status.GetAppConnectionStatus();
        }

        public async Task<IAppServiceResponse> SendMessageAsync(object message)
        {
            var jsonMessage = JsonConvert.SerializeObject(message);
            var messageSet = new ValueSet {{"message", jsonMessage}};
            var response = await _connection.SendMessageAsync(messageSet);
            return new AppServiceResponseWrapper(response.Message, response.Status.GetAppResponseStatus());
        }

        public void Dispose()
        {
            _connection?.Dispose();
            _connection = null;
        }
    }
}
