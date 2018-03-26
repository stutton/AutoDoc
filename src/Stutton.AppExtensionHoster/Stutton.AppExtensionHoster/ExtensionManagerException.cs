using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stutton.AppExtensionHoster
{
    public class ExtensionManagerException : Exception
    {
        public ExtensionManagerException() { }

        public ExtensionManagerException(string message) : base(message) { }

        public ExtensionManagerException(string message, Exception inner) : base(message, inner) { }
    }
}
