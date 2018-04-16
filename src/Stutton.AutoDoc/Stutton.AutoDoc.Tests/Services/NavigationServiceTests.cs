using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Stutton.AutoDoc.Logic.Services;
using Stutton.AutoDoc.Logic.Services.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stutton.AutoDoc.Tests.Services
{
    [TestClass]
    public class NavigationServiceTests
    {
        // Navigation service should:
        // 1. Navigate to object
        // 2. Navigate back to previous object
        // 3. Navigate forward to next object


        [TestMethod]
        public void NavigateToPage()
        {
            var pageFactory = Mock.Of<IPageFactory>(
                p => p.GetPage(It.IsAny<Type>()) == new PageMock());
            var nav = new NavigationService(pageFactory);

            nav.Navigate(typeof(PageMock));

            Assert.IsInstanceOfType(nav.CurrentPage, typeof(PageMock));
        }

        [TestMethod]
        public void NavigateToMultiplePages()
        {
            var pageFactory = Mock.Of<IPageFactory>(
                p => p.GetPage(It.Is<Type>(t => t == typeof(PageMock))) == new PageMock()
                  && p.GetPage(It.Is<Type>(t => t == typeof(PageMock2))) == new PageMock2());
            var nav = new NavigationService(pageFactory);

            nav.Navigate(typeof(PageMock));
            nav.Navigate(typeof(PageMock2));

            Assert.IsInstanceOfType(nav.CurrentPage, typeof(PageMock2));
        }

        [TestMethod]
        public void NavigateBack()
        {
            var pageFactory = Mock.Of<IPageFactory>(
                p => p.GetPage(It.Is<Type>(t => t == typeof(PageMock))) == new PageMock()
                  && p.GetPage(It.Is<Type>(t => t == typeof(PageMock2))) == new PageMock2());
            var nav = new NavigationService(pageFactory);

            nav.Navigate(typeof(PageMock));
            nav.Navigate(typeof(PageMock2));
            nav.GoBack();

            Assert.IsInstanceOfType(nav.CurrentPage, typeof(PageMock));
        }

        [TestMethod]
        public void NavigateBackWithNoHistory()
        {
            var pageFactory = Mock.Of<IPageFactory>(
                p => p.GetPage(It.IsAny<Type>()) == new PageMock());
            var nav = new NavigationService(pageFactory);

            nav.GoBack();

            Assert.IsNull(nav.CurrentPage);
        }

        [TestMethod]
        public void NavigateBackWithOnlyOnePageHistory()
        {
            var pageFactory = Mock.Of<IPageFactory>(
                p => p.GetPage(It.IsAny<Type>()) == new PageMock());
            var nav = new NavigationService(pageFactory);

            nav.Navigate(typeof(PageMock));
            nav.GoBack();

            Assert.IsInstanceOfType(nav.CurrentPage, typeof(PageMock));
        }

        [TestMethod]
        public void CanNavigateBackNoHistory()
        {
            var pageFactory = Mock.Of<IPageFactory>(
                p => p.GetPage(It.IsAny<Type>()) == new PageMock());
            var nav = new NavigationService(pageFactory);

            Assert.IsFalse(nav.CanGoBack);
        }

        [TestMethod]
        public void CanNavigateBackOnePageHistory()
        {
            var pageFactory = Mock.Of<IPageFactory>(
                p => p.GetPage(It.IsAny<Type>()) == new PageMock());
            var nav = new NavigationService(pageFactory);

            nav.Navigate(typeof(PageMock));

            Assert.IsFalse(nav.CanGoBack);
        }

        [TestMethod]
        public void CanNavigateBackMultiplePageHistory()
        {
            var pageFactory = Mock.Of<IPageFactory>(
                p => p.GetPage(It.Is<Type>(t => t == typeof(PageMock))) == new PageMock()
                  && p.GetPage(It.Is<Type>(t => t == typeof(PageMock2))) == new PageMock2());
            var nav = new NavigationService(pageFactory);

            nav.Navigate(typeof(PageMock));
            nav.Navigate(typeof(PageMock2));

            Assert.IsTrue(nav.CanGoBack);
        }

        [TestMethod]
        public void NavigateForward()
        {
            var pageFactory = Mock.Of<IPageFactory>(
                p => p.GetPage(It.Is<Type>(t => t == typeof(PageMock))) == new PageMock()
                  && p.GetPage(It.Is<Type>(t => t == typeof(PageMock2))) == new PageMock2());
            var nav = new NavigationService(pageFactory);

            nav.Navigate(typeof(PageMock));
            nav.Navigate(typeof(PageMock2));
            nav.GoBack();
            nav.GoForward();

            Assert.IsInstanceOfType(nav.CurrentPage, typeof(PageMock2));
        }

        [TestMethod]
        public void NavigateForwardNoForward()
        {
            var pageFactory = Mock.Of<IPageFactory>(
                p => p.GetPage(It.IsAny<Type>()) == new PageMock());
            var nav = new NavigationService(pageFactory);

            nav.GoForward();

            Assert.IsNull(nav.CurrentPage);
        }

        [TestMethod]
        public void NavigateForwardOneForward()
        {
            var pageFactory = Mock.Of<IPageFactory>(
                p => p.GetPage(It.IsAny<Type>()) == new PageMock());
            var nav = new NavigationService(pageFactory);

            nav.Navigate(typeof(PageMock));
            nav.GoForward();

            Assert.IsInstanceOfType(nav.CurrentPage, typeof(PageMock));
        }

        //[TestMethod]
        //public void CanNavigateForward()
        //{

        //}

        private class PageMock : IPage
        {
            public string Title => "Mock Page";
        }

        private class PageMock2 : IPage
        {
            public string Title => "Mock Page 2";
        }
    }
}
