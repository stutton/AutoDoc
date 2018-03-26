using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;
using Stutton.AppExtensionHoster.Contracts;

namespace Stutton.AppExtensionHoster.Uwp
{
    public class BitmapImageWrapper : IBitmapImage
    {
        public BitmapImage Image { get; }

        public BitmapImageWrapper(BitmapImage image)
        {
            Image = image;
        }
    }
}
