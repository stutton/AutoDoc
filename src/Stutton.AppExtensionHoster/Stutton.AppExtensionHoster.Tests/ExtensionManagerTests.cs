using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Stutton.AppExtensionHoster.Contracts;

namespace Stutton.AppExtensionHoster.Tests
{
    [TestClass]
    public class ExtensionManagerTests
    {
        [TestMethod]
        public void VerifyContractname()
        {
            // Arrange
            var mockCatalog = new Mock<IAppExtensionCatalog>();
            var mockConnectionFactory = new Mock<IAppServiceConnectionFactory>();

            // Act
            var manager = new ExtensionManager<string, string>(
                "MockContractName", 
                new List<SignatureKind>
                {
                    SignatureKind.Developer,
                    SignatureKind.None,
                    SignatureKind.Store,
                    SignatureKind.System
                },
                mockCatalog.Object,
                mockConnectionFactory.Object);

            // Assert
            Assert.AreEqual("MockContractName", manager.ContractName);
        }

        [TestMethod]
        public void Test()
        {
            // Arrange

            // Act

            // Assert
        }
    }
}
