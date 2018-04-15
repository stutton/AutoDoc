using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Stutton.AutoDoc.Logic;

namespace Stutton.AutoDoc.Tests
{
    [TestClass]
    public class ObservableTests
    {
        [TestMethod]
        public void PropertyChangedRaisedWhenPropertyChanges()
        {
            var notifyObject = new ObservableMock();
            var propertyChangedRaised = false;
            notifyObject.PropertyChanged += (s, e) => propertyChangedRaised = true;

            notifyObject.NotifyProp = "test";

            Assert.IsTrue(propertyChangedRaised);
        }

        [TestMethod]
        public void PropertyChangedEventArgsHasCorrectPropertyName()
        {
            var notifyObject = new ObservableMock();
            string propertyName = null;
            notifyObject.PropertyChanged += (s, e) => propertyName = e.PropertyName;

            notifyObject.NotifyProp = "test";

            Assert.AreEqual("NotifyProp", propertyName);
        }

        [TestMethod]
        public void PropertyChangedEventArgsHasOverriddenPropertyName()
        {
            var notifyObject = new ObservableMock();
            string overriddenName = null;
            notifyObject.PropertyChanged += (s, e) => overriddenName = e.PropertyName;

            notifyObject.NotifyPropOverride = "test";

            Assert.AreEqual("OverriddenName", overriddenName);
        }

        [TestMethod]
        public void PropertyChangedSenderIsCorrectObject()
        {
            var notifyObject = new ObservableMock();
            object sender = null;
            notifyObject.PropertyChanged += (s, e) => sender = s;

            notifyObject.NotifyProp = "test";

            Assert.AreSame(notifyObject, sender);
        }

        [TestMethod]
        public void PropertyChangedNotRaisedIfNewValueEqualsOldValue()
        {
            var notifyObject = new ObservableMock();
            var propertyChangedRaised = false;
            notifyObject.NotifyProp = "test";
            notifyObject.PropertyChanged += (s, e) => propertyChangedRaised = true;

            notifyObject.NotifyProp = "test";

            Assert.IsFalse(propertyChangedRaised);
        }

        private class ObservableMock : Observable
        {
            private string _notifyProp;

            public string NotifyProp
            {
                get => _notifyProp;
                set => SetProperty(ref _notifyProp, value);
            }

            private string _notifyPropOverride;

            public string NotifyPropOverride
            {
                get => _notifyPropOverride;
                set => SetProperty(ref _notifyPropOverride, value, "OverriddenName");
            }
        }
    }
}
