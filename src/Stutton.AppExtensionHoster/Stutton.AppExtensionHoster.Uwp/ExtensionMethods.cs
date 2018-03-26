using System;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.AppExtensions;
using Windows.ApplicationModel.AppService;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml.Media.Imaging;
using Stutton.AppExtensionHoster.Contracts;

namespace Stutton.AppExtensionHoster.ContractImplementations
{
    internal static class ExtensionMethods
    {
        public static string GetUniqueId(this AppExtension extension)
        {
            return extension.AppInfo.AppUserModelId + "!" + extension.Id;
        }

        public static async Task<BitmapImage> GetLogoAsync(this AppExtension extension)
        {
            var logo = new BitmapImage();
            var fileStream = await extension.AppInfo.DisplayInfo.GetLogo(new Size(1, 1)).OpenReadAsync();
            await logo.SetSourceAsync(fileStream);
            return logo;
        }

        public static async Task<string> GetServiceNameAsync(this AppExtension extension)
        {
            string result = null;

            if (await extension.GetExtensionPropertiesAsync() is PropertySet properties)
            {
                if (properties.ContainsKey("Service"))
                {
                    if (properties["Service"] is PropertySet serviceProperty)
                    {
                        result = serviceProperty["#text"]?.ToString();
                    }
                }
            }

            return result;
        }

        public static AppConnectionStatus GetAppConnectionStatus(this AppServiceConnectionStatus status)
        {
            switch (status)
            {
                case AppServiceConnectionStatus.Success:
                    return AppConnectionStatus.Success;
                case AppServiceConnectionStatus.AppNotInstalled:
                    return AppConnectionStatus.AppNotInstalled;
                case AppServiceConnectionStatus.AppUnavailable:
                    return AppConnectionStatus.AppUnavailable;
                case AppServiceConnectionStatus.AppServiceUnavailable:
                    return AppConnectionStatus.AppServiceUnavailable;
                case AppServiceConnectionStatus.Unknown:
                    return AppConnectionStatus.Unknown;
                case AppServiceConnectionStatus.RemoteSystemUnavailable:
                    return AppConnectionStatus.RemoteSystemUnavailable;
                case AppServiceConnectionStatus.RemoteSystemNotSupportedByApp:
                    return AppConnectionStatus.RemoteSystemNotSupportedByApp;
                case AppServiceConnectionStatus.NotAuthorized:
                    return AppConnectionStatus.NotAuthorized;
                default:
                    throw new ArgumentOutOfRangeException(nameof(status), status, null);
            }
        }

        public static AppResponseStatus GetAppResponseStatus(this AppServiceResponseStatus status)
        {
            switch (status)
            {
                case AppServiceResponseStatus.Success:
                    return AppResponseStatus.Success;
                case AppServiceResponseStatus.Failure:
                    return AppResponseStatus.Failure;
                case AppServiceResponseStatus.ResourceLimitsExceeded:
                    return AppResponseStatus.ResourceLimitsExceeded;
                case AppServiceResponseStatus.Unknown:
                    return AppResponseStatus.Unknown;
                case AppServiceResponseStatus.RemoteSystemUnavailable:
                    return AppResponseStatus.RemoteSystemUnavailable;
                case AppServiceResponseStatus.MessageSizeTooLarge:
                    return AppResponseStatus.MessageSizeTooLarge;
                default:
                    throw new ArgumentOutOfRangeException(nameof(status), status, null);
            }
        }

        public static CoreDispatcherPriority GetCoreDispatcherPriority(this DispatcherPriority priority)
        {
            switch (priority)
            {
                case DispatcherPriority.Idle:
                    return CoreDispatcherPriority.Idle;
                case DispatcherPriority.Low:
                    return CoreDispatcherPriority.Low;
                case DispatcherPriority.Normal:
                    return CoreDispatcherPriority.Normal;
                case DispatcherPriority.High:
                    return CoreDispatcherPriority.High;
                default:
                    throw new ArgumentOutOfRangeException(nameof(priority), priority, null);
            }
        }

        public static SignatureKind GetSignatureKind(this PackageSignatureKind kind)
        {
            switch (kind)
            {
                case PackageSignatureKind.None:
                    return SignatureKind.None;
                case PackageSignatureKind.Developer:
                    return SignatureKind.Developer;
                case PackageSignatureKind.Enterprise:
                    return SignatureKind.Enterprise;
                case PackageSignatureKind.Store:
                    return SignatureKind.Store;
                case PackageSignatureKind.System:
                    return SignatureKind.System;
                default:
                    throw new ArgumentOutOfRangeException(nameof(kind), kind, null);
            }
        }
    }
}
