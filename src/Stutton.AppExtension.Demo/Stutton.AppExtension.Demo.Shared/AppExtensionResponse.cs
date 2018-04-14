using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Stutton.AppExtension.Demo.Shared
{
    [DataContract]
    public class AppExtensionResponse
    {
        [DataMember]
        public double Result { get; set; }
    }
}
