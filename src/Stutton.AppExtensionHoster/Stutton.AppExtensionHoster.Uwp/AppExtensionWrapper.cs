using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.AppExtensions;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml.Media.Imaging;
using Stutton.AppExtensionHoster.Contracts;
using Stutton.AppExtensionHoster.Uwp;

namespace Stutton.AppExtensionHoster.ContractImplementations
{
    public class AppExtensionWrapper : IAppExtension
    {
        public AppExtensionWrapper(AppExtension extension)
        {
            WrappedExtension = extension;
            Package = new AppPackageWrapper(WrappedExtension.Package);
        }

        public AppExtension WrappedExtension { get; }

        public IAppPackage Package { get; }

        public async Task<IDictionary<string, object>> GetExtensionPropertiesAsync()
        {
            var properties = await WrappedExtension.GetExtensionPropertiesAsync();
            return properties.ToDictionary(p => p.Key, p => p.Value);
        }

        public Task<string> GetServiceNameAsync() => WrappedExtension.GetServiceNameAsync();

        public async Task<IBitmapImage> GetLogoAsync()
        {
            var logo = await WrappedExtension.GetLogoAsync();
            return new BitmapImageWrapper(logo);
        }

        public string GetUniqueId() => WrappedExtension.GetUniqueId();
        public IAppServiceConnection GetConnection()
        {
            throw new NotImplementedException();
        }
    }
}
