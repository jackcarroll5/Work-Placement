using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SearcherApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NameReloader : ContentPage
	{
		public NameReloader ()
		{
			InitializeComponent ();

            listNameView.ItemsSource = new List<string>()
            {
                "Smith",
                "Canoli"
            };
		}

        private void ListNameView_Refreshing(object sender, EventArgs e)
        {
            listNameView.ItemsSource = new List<string>()
            {
                "Smith",
                "Canoli",
                "Edwards",
                "Murphy"
            };

            listNameView.IsRefreshing = false;

        }

        private void ListNameView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            DisplayAlert("Chosen Name", e.SelectedItem.ToString(),"OK");
        }
    }
}