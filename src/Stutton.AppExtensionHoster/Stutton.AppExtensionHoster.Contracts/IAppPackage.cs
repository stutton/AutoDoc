using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stutton.AppExtensionHoster.Contracts
{
    public interface IAppPackage
    {
        SignatureKind SignatureKind { get; }
        string FamilyName { get; }
        string FullName { get; }
        bool Offline { get; }
        bool Servicing { get; }
        bool DeploymentInProgress { get; }

        bool VerifyIsOK();
    }
}
