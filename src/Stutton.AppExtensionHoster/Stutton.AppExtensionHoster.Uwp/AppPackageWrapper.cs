using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Stutton.AppExtensionHoster.Contracts;

namespace Stutton.AppExtensionHoster.ContractImplementations
{
    public class AppPackageWrapper : IAppPackage
    {
        private readonly Package _appPackage;

        public AppPackageWrapper(Package appPackage)
        {
            _appPackage = appPackage;
        }

        public SignatureKind SignatureKind => _appPackage.SignatureKind.GetSignatureKind();
        public string FamilyName => _appPackage.Id.FamilyName;
        public string FullName => _appPackage.Id.FullName;
        public bool Offline => _appPackage.Status.PackageOffline;
        public bool Servicing => _appPackage.Status.Servicing;
        public bool DeploymentInProgress => _appPackage.Status.DeploymentInProgress;

        public bool VerifyIsOK() => _appPackage.Status.VerifyIsOK();
    }
}
