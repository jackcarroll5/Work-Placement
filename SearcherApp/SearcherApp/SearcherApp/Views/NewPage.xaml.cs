using SearcherApp.Models;
using SearcherApp.ViewModels;
using SearcherApp.Views;
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
	public partial class NewPage : ContentPage
	{
        public NewPage()
        {
            InitializeComponent();

        }

        private async void Button_ClickedAsync(object sender, EventArgs e)
        {
            var button = sender as Button;

            var people = button?.BindingContext as People;

            var vm = BindingContext as PeopleViewModel;

            var answer = await DisplayAlert("Attention", "Are you sure you want to remove this name?", "Yes", "No");

            if(answer == true)
            {
                vm?.RemoveCommand.Execute(people);
            }         
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new TabbingPage());
        }
    }
}