using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stutton.AppExtensionClient
{
    public class ExtensionClientException : Exception
    {
        public ExtensionClientException() { }

        public ExtensionClientException(string message) : base(message) { }

        public ExtensionClientException(string message, Exception inner) : base(message, inner) { }
    }
}
