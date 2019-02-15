using MVVMPrismApp.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MVVMPrismApp.ViewModels
{
    public class BlogViewModel : ViewModelBase
    {
        public BlogViewModel(INavigationService navigationService)
            : base(navigationService)
        {

        }

        private Blog blogDetail;

        public Blog BlogDetails
        {
            get
            {
                return blogDetail;
            }
            set
            {
                SetProperty(ref blogDetail, value);
            }
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            BlogDetails = (Blog)parameters["Blog"];
            Title = BlogDetails.BlogTitle;
            base.OnNavigatedTo(parameters);
        }
    }
}