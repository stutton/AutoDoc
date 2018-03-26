using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.AppExtensions;
using Windows.ApplicationModel.AppService;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml.Media.Imaging;
using Newtonsoft.Json;
using Stutton.AppExtensionHoster.Contracts;

namespace Stutton.AppExtensionHoster
{
    public class Extension<TMessage, TResponse> : NotifyBase
    {
        private PropertySet _properties;
        private string _serviceName;
        private readonly object _sync = new object();

        public Extension(IAppExtension extension)
        {
            AppExtension = extension;
            UniqueId = extension.GetUniqueId();
            _state = ExtensionState.Uninitialized;
        }

        public string UniqueId { get; }

        private BitmapImage _logo;
        public BitmapImage Logo
        {
            get => _logo;
            private set => SetProperty(ref _logo, value);
        }

        public IAppExtension AppExtension { get; private set; }

        private ExtensionState _state;
        public ExtensionState State
        {
            get => _state;
            set => SetProperty(ref _state, value);
        }

        public async Task<(string Status, string Message, TResponse Response)> Invoke(TMessage message)
        {
            if (State != ExtensionState.Ready)
            {
                return ("error", $"Extension for {_serviceName} is not ready: Extension status is {State}", default(TResponse));
            }

            try
            {
                var messageSet = new ValueSet {{"message", message}};
                using (var connection = new AppServiceConnection())
                {
                    connection.AppServiceName = _serviceName;
                    connection.PackageFamilyName = AppExtension.Package.FamilyName;

                    var status = await connection.OpenAsync();

                    #region Error Handling
                    if (status != AppServiceConnectionStatus.Success)
                    {
                        return ("error", $"Failed to open app service connection to {_serviceName}: Connection status is {status}", default(TResponse));
                    }
                    #endregion

                    var response = await connection.SendMessageAsync(messageSet);

                    #region Error Handling
                    if (response.Status != AppServiceResponseStatus.Success)
                    {
                        return ("error", $"Failed to call app service {_serviceName}: Reponse status is {response.Status}", default(TResponse));
                    }
                    if (!response.Message.ContainsKey("result"))
                    {
                        return ("error", $"Response from {_serviceName} did not contain a result", default(TResponse));
                    }
                    #endregion

                    var resultObj = response.Message["result"];
                    if (resultObj is TResponse result)
                    {
                        return ("success", null, result);
                    }

                    #region Error Handling
                    return ("error",
                        $"Response message from app service {_serviceName} was not of the expected type '{typeof(TResponse).Name}': Received message of type '{resultObj.GetType().Name}'",
                        default(TResponse));
                    #endregion
                }

            }
            catch (Exception ex)
            {
                return ("error", $"An error occured invoking extension for {_serviceName}: {ex.Message}",
                    default(TResponse));
            }
        }

        internal async Task Initialize()
        {
            if (State != ExtensionState.Uninitialized)
            {
                throw new ExtensionException($"Extension for '{_serviceName}' is already initialized");
            }

            _properties = await AppExtension.GetExtensionPropertiesAsync() as PropertySet;
            _serviceName = await AppExtension.GetServiceNameAsync();
            _logo = await AppExtension.GetLogoAsync();
            State = ExtensionState.Offline;
        }

        internal void Load()
        {
            if (State == ExtensionState.Uninitialized)
            {
                throw new ExtensionException($"Extension for '{_serviceName}' must be initialized before it can be loaded");
            }

            if (!AppExtension.Package.VerifyIsOK())
            {
                State = ExtensionState.Offline;
                return;
            }

            State = ExtensionState.Ready;
        }

        public void Enable()
        {
            Load();
        }

        public void Disable()
        {
            if (State == ExtensionState.Ready)
            {
                State = ExtensionState.Disabled;
                Unload();
            }
        }

        internal async Task Update(IAppExtension newExtension)
        {
            var identifier = newExtension.GetUniqueId();
            if (identifier != UniqueId)
            {
                return;
            }

            State = ExtensionState.Offline;

            AppExtension = newExtension;
            _properties = await newExtension.GetExtensionPropertiesAsync() as PropertySet;
            _serviceName = await newExtension.GetServiceNameAsync();
            Logo = await newExtension.GetLogoAsync();

            Load();
        }

        internal void Unload()
        {
            lock (_sync)
            {
                if (State != ExtensionState.Offline)
                {
                    if (!AppExtension.Package.VerifyIsOK() || AppExtension.Package.Offline)
                    {
                        State = ExtensionState.Offline;
                    }

                    State = ExtensionState.Disabled;
                }
            }
        }
    }
}
