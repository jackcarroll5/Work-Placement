using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SearcherApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NamePage : ContentPage
	{
		public NamePage ()
		{
			InitializeComponent ();
		}

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SwipeForever());
        }
    }
}