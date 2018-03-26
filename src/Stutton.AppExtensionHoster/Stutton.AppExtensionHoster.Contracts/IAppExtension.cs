using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml.Media.Imaging;

namespace Stutton.AppExtensionHoster.Contracts
{
    public interface IAppExtension
    {
        IAppPackage Package { get; }

        IAsyncOperation<IPropertySet> GetExtensionPropertiesAsync();
        Task<string> GetServiceNameAsync();
        Task<BitmapImage> GetLogoAsync();
        string GetUniqueId();
    }
}
