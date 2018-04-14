using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stutton.AppExtensionHoster.Contracts;

namespace Stutton.AppExtensionHoster.Uwp
{
    public class AppServiceConnectionFactor : IAppServiceConnectionFactory
    {
        public IAppServiceConnection Create()
        {
            return new AppServiceConnectionWrapper();
        }
    }
}
