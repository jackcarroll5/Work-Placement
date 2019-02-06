using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NavPageTut
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Page1 : ContentPage
	{
		public Page1 ()
		{
			InitializeComponent ();
		}

        private void Button_Click(object sender, EventArgs e)
        {
            var page = new Page2();

            NavigationPage.SetBackButtonTitle(page, "P2Back");

            //NavigationPage.SetHasNavigationBar(page, false);

          // NavigationPage.SetHasBackButton(page, true);

            Navigation.PushAsync(page);
        }

        private void Button_Clicked3(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Page3());
        }
    }
}