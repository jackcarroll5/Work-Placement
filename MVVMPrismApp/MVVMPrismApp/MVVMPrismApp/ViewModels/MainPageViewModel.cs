using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVVMPrismApp.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public DelegateCommand NavToSecond { get; set; }

        private INavigationService navService;

        public MainPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Main Page";
            navService = navigationService;
            NavToSecond = new DelegateCommand(NavToSecondCall);
        }

        public void NavToSecondCall()
        {
            navService.NavigateAsync("NewPage");
        }



    }
}
