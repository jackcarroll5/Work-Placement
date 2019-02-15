
using MVVMPrismApp.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MVVMPrismApp.ViewModels
{
	public class NewPageViewModel : ViewModelBase
	{
        public DelegateCommand NavToBlogCommand { get; private set; }

        private List<Blog> blogs = new List<Blog>()
        {
            new Blog
            {
                Description = "olestie lectus rhoncus non. Ut eget metus neque. Cras laoreet quam ligula, in ultricies enim lobortis vitae. Proin sed justo vel quam luctus bibendum. Praesent gravida vehicula nunc, eu aliquet elit vestibulum non. Aliquam aliquam fringilla nunc, eget tincidunt dui finibus vel.",
                BlogTitle = "Blog 1",
                DateCreated = DateTime.Now,
                Creator = "Ryan Nguyen"
            },

             new Blog
            {
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam a sollicitudin erat. Vestibulum dapibus sagittis risus, sit amet molestie lectus rhoncus non. Ut eget metus neque. Cras laoreet quam ligula, in ultricies enim lobortis vitae. Proin sed justo vel quam luctus bibendum. Praesent gravida vehicula nunc, eu aliquet elit vestibulum non. Aliquam aliquam fringilla nunc, eget tincidunt dui finibus vel. Ut fermentum viverra justo, a vehicula dui dignissim non. Aenean posuere vestibulum felis, eu fringilla nulla. Nam interdum enim nec risus cursus tempus. Integer vestibulum hendrerit metus, a venenatis justo molestie at. Vestibulum nec libero in velit pulvinar pretium non in enim. Nulla pulvinar auctor dolor, eu laoreet quam vehicula nec. Cras vehicula fringilla massa, vitae molestie lectus dignissim sed. Integer suscipit auctor est, eu bibendum turpis aliquam vitae.",
                BlogTitle = "Blog 2",
                DateCreated = new DateTime(2018,12,30),
                Creator = "Adam Costenbader"
            },
            new Blog
            {
                Description = "Etiam a sollicitudin erat. Vestibulum dapibus sagittis risus, sit amet molestie lectus rhoncus non. Ut eget metus neque. Cras laoreet quam ligula, in ultricies enim lobortis vitae. Proin sed justo vel quam luctus bibendum. Praesent gravida vehicula nunc, eu aliquet elit vestibulum non. Aliquam aliquam fringilla nunc, eget tincidunt dui finibus vel. Ut fermentum viverra justo, a vehicula dui dignissim non. Aenean posuere vestibulum felis, eu fringilla nulla. Nam interdum enim nec risus cursus tempus. Integer vestibulum hendrerit metus, a venenatis justo molestie at. Vestibulum nec libero in velit pulvinar pretium non in enim. Nulla pulvinar auctor dolor, eu laoreet quam vehicula nec. Cras vehicula fringilla massa, vitae molestie lectus dignissim sed. Integer suscipit auctor est, eu bibendum turpis aliquam vitae.",
                BlogTitle = "Blog 3",
                DateCreated = new DateTime(2018,11,30),
                Creator = "Rusty Divine"
            },
            new Blog
            {
               Description = "molestie lectus rhoncus non. Ut eget metus neque. Cras laoreet quam ligula, in ultricies enim lobortis vitae. Proin sed justo vel quam luctus bibendum. Praesent gravida vehicula nunc, eu aliquet elit vestibulum non. Aliquam aliquam fringilla nunc, eget tincidunt dui finibus vel. Ut fermentum viverra justo, a vehicula dui dignissim non. Aenean posuere vestibulum felis, eu fringilla nulla. Nam interdum enim nec risus cursus tempus. Integer vestibulum hendrerit metus, a venenatis justo molestie at. Vestibulum nec libero in velit pulvinar pretium non in enim. Nulla pulvinar auctor dolor, eu laoreet quam vehicula nec. Cras vehicula fringilla massa, vitae molestie lectus dignissim sed. Integer suscipit auctor est, eu bibendum turpis aliquam vitae.",
                BlogTitle = "Blog 4",
                DateCreated = new DateTime(2018,10,30),
                Creator = "Linh Nguyen"
            }

        };

        public List<Blog> Blogs
        {
            get
            {
                return blogs;
            }
            set
            {
                SetProperty(ref blogs, value);
            }
        }

        private Blog blogSelected;

        public Blog BlogSelected
        {
            get
            {
                return blogSelected;
            }
            set
            {
                SetProperty(ref blogSelected, value);
            }
        }

        public NewPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Blogs";
            NavToBlogCommand = new DelegateCommand(NavToBlog, () => BlogSelected != null).ObservesProperty(() => BlogSelected);
        }

        public void NavToBlog()
        {
            var param = new NavigationParameters();
            param.Add("Blog", BlogSelected);
            NavigationService.NavigateAsync("Blog", param);
        }
	}
}
