using Stutton.AutoDoc.Logic.Services.Navigation;
using System;

namespace Stutton.AutoDoc.Logic.ViewModels
{
    public class ShellViewModel : Observable
    {
        private readonly NavigationService _navigationService;

        public ShellViewModel(NavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public IPage CurrentPage => _navigationService.CurrentPage;
    }
}
