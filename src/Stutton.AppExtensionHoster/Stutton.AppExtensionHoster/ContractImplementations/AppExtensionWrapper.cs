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

        public IAsyncOperation<IPropertySet> GetExtensionPropertiesAsync() => WrappedExtension.GetExtensionPropertiesAsync();

        public Task<string> GetServiceNameAsync() => WrappedExtension.GetServiceNameAsync();

        public Task<BitmapImage> GetLogoAsync() => WrappedExtension.GetLogoAsync();

        public string GetUniqueId() => WrappedExtension.GetUniqueId();
    }
}
