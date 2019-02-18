using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace DepService
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();



            var content = new ContentPage
            {
                Title = "Native Dependency",
                Content = new StackLayout
                {
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,

                    Children =
                    {
                        new Label
                        {
                            HorizontalTextAlignment = TextAlignment.Center,
                            Text = DependencyService.Get<IDepDemo>().GetPlatformMessage()
                        }
                    }
                }
            };

            MainPage = new NavigationPage(content);

        }

       
           
        

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
