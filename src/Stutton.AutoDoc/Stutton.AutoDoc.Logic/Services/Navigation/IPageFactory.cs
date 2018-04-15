using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stutton.AutoDoc.Logic.Services.Navigation
{
    public interface IPageFactory
    {
        IPage GetPage(Type pageType);
    }
}
