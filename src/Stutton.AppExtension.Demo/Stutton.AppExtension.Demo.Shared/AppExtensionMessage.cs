using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation.Collections;

namespace Stutton.AppExtension.Demo.Shared
{
    [DataContract]
    public class AppExtensionMessage
    {
        [DataMember]
        public double[] Parameters { get; set; }
    }
}
