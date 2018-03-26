using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stutton.AppExtensionHoster
{
    public class ExtensionException : Exception
    {
        public ExtensionException() { }

        public ExtensionException(string message) : base(message) { }

        public ExtensionException(string message, Exception inner) : base(message, inner) { }
    }
}
