using System;

namespace Stutton.AutoDoc.Logic.ViewModels
{
    public class ShellViewModel : Observable
    {
        private int _test = 42;

        public int Test { get => _test; set => SetProperty(ref _test, value); }
    }
}
