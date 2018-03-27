using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        public async Task InitilizeWithNoExtensions()
        {
            // Arrange
            var mockCatalog = new Mock<IAppExtensionCatalog>();
            mockCatalog.Setup(c => c.FindAllAsync()).Returns(Task.FromResult((IReadOnlyList<IAppExtension>)new List<IAppExtension>()));

            var mockConnectionFactory = new Mock<IAppServiceConnectionFactory>();
            var mockDispatcher = new Mock<IDispatcher>();

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

            // Act
            await manager.InitializeAsync(mockDispatcher.Object);

            // Assert
            Assert.AreEqual(0, manager.Extensions.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ExtensionManagerException), "ExtensionManager for MockContractName is already initialized")]
        public async Task InitilizeTwiceThrowsException()
        {
            // Arrange
            var mockCatalog = new Mock<IAppExtensionCatalog>();
            mockCatalog.Setup(c => c.FindAllAsync()).Returns(Task.FromResult((IReadOnlyList<IAppExtension>)new List<IAppExtension>()));

            var mockConnectionFactory = new Mock<IAppServiceConnectionFactory>();
            var mockDispatcher = new Mock<IDispatcher>();

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

            // Act
            await manager.InitializeAsync(mockDispatcher.Object);
            await manager.InitializeAsync(mockDispatcher.Object);
        }

        [TestMethod]
        public async Task InitilizeWithExtensions()
        {
            // Arrange
            var catalog = Mock.Of<IAppExtensionCatalog>(
                c => c.FindAllAsync() == Task.FromResult(
                         (IReadOnlyList<IAppExtension>) new List<IAppExtension>
                         {
                             Mock.Of<IAppExtension>(
                                 e => e.GetUniqueId() == "a1" &&
                                      e.Package == Mock.Of<IAppPackage>(
                                          p => p.VerifyIsOK() == true)),
                             Mock.Of<IAppExtension>(
                                 e => e.GetUniqueId() == "a2" &&
                                      e.Package == Mock.Of<IAppPackage>(
                                          p => p.VerifyIsOK() == true))
                         }
                     )
            );

            var connectionFactory = Mock.Of<IAppServiceConnectionFactory>();
            var dispatcher = Mock.Of<IDispatcher>();

            var manager = new ExtensionManager<string, string>(
                "MockContractName",
                new List<SignatureKind>
                {
                    SignatureKind.Developer,
                    SignatureKind.None,
                    SignatureKind.Store,
                    SignatureKind.System
                },
                catalog,
                connectionFactory);

            // Act
            await manager.InitializeAsync(dispatcher);

            // Assert
            Assert.AreEqual(2, manager.Extensions.Count);
        }

        [TestMethod]
        public async Task InitilizeWithExtensionNotOk()
        {
            // Arrange
            var catalog = Mock.Of<IAppExtensionCatalog>(
                c => c.FindAllAsync() == Task.FromResult(
                         (IReadOnlyList<IAppExtension>)new List<IAppExtension>
                         {
                             Mock.Of<IAppExtension>(
                                 e => e.GetUniqueId() == "a1" &&
                                      e.Package == Mock.Of<IAppPackage>(
                                          p => p.VerifyIsOK() == false))
                         }
                     )
            );

            var connectionFactory = Mock.Of<IAppServiceConnectionFactory>();
            var dispatcher = Mock.Of<IDispatcher>();

            var manager = new ExtensionManager<string, string>(
                "MockContractName",
                new List<SignatureKind>
                {
                    SignatureKind.Developer,
                    SignatureKind.None,
                    SignatureKind.Store,
                    SignatureKind.System
                },
                catalog,
                connectionFactory);

            // Act
            await manager.InitializeAsync(dispatcher);

            // Assert
            Assert.AreEqual(0, manager.Extensions.Count);
        }

        [TestMethod]
        public async Task InitializeWithExtensionVerifyReady()
        {
            // Arrange
            var catalog = Mock.Of<IAppExtensionCatalog>(
                c => c.FindAllAsync() == Task.FromResult(
                         (IReadOnlyList<IAppExtension>)new List<IAppExtension>
                         {
                             Mock.Of<IAppExtension>(
                                 e => e.GetUniqueId() == "a1" &&
                                      e.Package == Mock.Of<IAppPackage>(
                                          p => p.VerifyIsOK() == true))
                         }
                     )
            );

            var connectionFactory = Mock.Of<IAppServiceConnectionFactory>();
            var dispatcher = Mock.Of<IDispatcher>();

            var manager = new ExtensionManager<string, string>(
                "MockContractName",
                new List<SignatureKind>
                {
                    SignatureKind.Developer,
                    SignatureKind.None,
                    SignatureKind.Store,
                    SignatureKind.System
                },
                catalog,
                connectionFactory);

            // Act
            await manager.InitializeAsync(dispatcher);

            // Assert
            Assert.AreEqual(manager.Extensions[0].State, ExtensionState.Ready);
        }

        [TestMethod]
        public async Task VerifyExtensionAddedWhenPackageInstalledRaised()
        {
            // Arrange
            var mockCatalog = new Mock<IAppExtensionCatalog>();
            mockCatalog.Setup(
                c => c.FindAllAsync()).Returns(Task.FromResult(
                    (IReadOnlyList<IAppExtension>) new List<IAppExtension>()
                )
            );
            var catalog = mockCatalog.Object;

            var connectionFactory = Mock.Of<IAppServiceConnectionFactory>();
            var mockDispatcher = new Mock<IDispatcher>();
            mockDispatcher.Setup(d => d.RunAsync(It.IsAny<DispatcherPriority>(), It.IsAny<Action>()))
                .Callback<DispatcherPriority, Action>(
                    (priority, action) => action());
            var dispatcher = mockDispatcher.Object;

            var eventArgs = Mock.Of<IAppExtensionPackageInstalledEventArgs>(
                args => args.Extensions ==
                        new List<IAppExtension>
                        {
                            Mock.Of<IAppExtension>(
                                e => e.GetUniqueId() == "a1" &&
                                     e.Package == Mock.Of<IAppPackage>(
                                         p => p.VerifyIsOK() == true))
                        }
            );

            var manager = new ExtensionManager<string, string>(
                "MockContractName",
                new List<SignatureKind>
                {
                    SignatureKind.Developer,
                    SignatureKind.None,
                    SignatureKind.Store,
                    SignatureKind.System
                },
                catalog,
                connectionFactory);

            await manager.InitializeAsync(dispatcher);

            // Act
            mockCatalog.Raise(
                c => c.PackageInstalled += null, null, eventArgs);

            // Assert
            Assert.AreEqual(1, manager.Extensions.Count);
        }
    }
}
