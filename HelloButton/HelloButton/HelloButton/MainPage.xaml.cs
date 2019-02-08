using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HelloButton
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void SayHelloButtonOnClicked(object sender, EventArgs e)
        {
            var name = NameEntry.Text;
            await DisplayAlert("Greetings", $"Hello {name}!", "Howdy");
        }



    }
}
