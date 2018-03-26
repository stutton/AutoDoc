using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stutton.AppExtensionHoster.Contracts
{
    public interface IAppExtension
    {
        IAppPackage Package { get; }

        Task<IDictionary<string, object>> GetExtensionPropertiesAsync();
        Task<string> GetServiceNameAsync();
        Task<IBitmapImage> GetLogoAsync();
        string GetUniqueId();
        IAppServiceConnection GetConnection();
    }
}
