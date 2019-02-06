using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ContextActionApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            ListViewMain.ItemsSource = new List<string>
            {
              "James", "Marcus", "Shauna", "Michelle", "Fred", "Brad",
              "Olivia", "Joe", "Emily", "Mike", "Dave"
            };
        }

        private void Add_OnClicked(object sender, EventArgs e)
        {
            var menuItem = sender as MenuItem;

            if(menuItem != null)
            {
              var name =  menuItem.BindingContext as string;

                DisplayAlert("Alert", "Add " + name, "OK");
            }
        }

        private void Edit_OnClicked(object sender, EventArgs e)
        {
            var menuItem = sender as MenuItem;

            if (menuItem != null)
            {
                var name = menuItem.BindingContext as string;

                DisplayAlert("Alert", "Edit " + name, "OK");
            }
        }

        private void Remove_OnClicked(object sender, EventArgs e)
        {
            var menuItem = sender as MenuItem;

            if (menuItem != null)
            {
                var name = menuItem.BindingContext as string;

                DisplayAlert("Alert", "Remove " + name, "OK");
            }
        }
    }
}
