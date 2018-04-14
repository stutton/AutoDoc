using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using Stutton.AppExtensionClient.Contracts;

namespace Stutton.AppExtensionClient.Tests
{
    [TestClass]
    public class ExtensionClientTests
    {
        private class MockExtensionClient : ExtensionClient<string, string>
        {
            protected override string Run(string message)
            {
                return message;
            }
        }

        [TestMethod]
        public void VerifyResultIsReturnedWhenRequestReceived()
        {
            // Arrange
            var client = new MockExtensionClient();

            var mockConnection = new Mock<IAppServiceConnection>();

            var task = Mock.Of<IAppTaskInstance>(
                t => t.TriggerDetails 
                     == Mock.Of<IAppServiceTriggerDetails>(
                         td => td.AppServiceConnection == mockConnection.Object
                               )
                     && t.GetDeferral()
                     == Mock.Of<IAppServiceDeferral>());

            var mockEventArgs = new Mock<IAppServiceRequestReceivedEventArgs>();
            mockEventArgs.Setup(a => a.Reqeust).Returns(Mock.Of<IAppServiceRequest>(
                r => r.Message == new Dictionary<string, object> {{"message", "mock message"}}));
            mockEventArgs.Setup(args => args.GetDeferral()).Returns(Mock.Of<IAppServiceDeferral>());

            client.Initialize(task);

            // Act
            mockConnection.Raise(c => c.RequestReceived += null, null, mockEventArgs.Object);

            // Assert
            mockEventArgs.Verify(args =>
                args.SendResponseAsync(
                    It.Is<Dictionary<string, object>>(p => p["result"].ToString() == "mock message")));
        }
    }
}
