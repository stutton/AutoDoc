using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.AppExtensions;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml.Media.Imaging;

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
    }
}
