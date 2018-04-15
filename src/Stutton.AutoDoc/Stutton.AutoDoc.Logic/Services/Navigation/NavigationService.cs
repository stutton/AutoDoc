using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stutton.AutoDoc.Logic.Services.Navigation
{
    public sealed class NavigationService
    {
        private readonly IPageFactory _pageFactory;

        private readonly Stack<Type> _pageHistory = new Stack<Type>();

        public NavigationService(IPageFactory pageFactory)
        {
            _pageFactory = pageFactory;
        }

        public IPage CurrentPage { get; set; }

        public void Navigate(Type pageType)
        {
            if(CurrentPage != null)
            {
                _pageHistory.Push(CurrentPage.GetType());
            }

            CurrentPage = _pageFactory.GetPage(pageType);
        }

        public void GoBack()
        {
            if(_pageHistory.Count == 0)
            {
                return;
            }

            CurrentPage = _pageFactory.GetPage(_pageHistory.Pop());
        }

        public bool CanGoBack => _pageHistory.Count >= 2;
    }
}
