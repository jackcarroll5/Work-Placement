using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SearcherColourApp
{
    public partial class MainPage : ContentPage
    {
        List<string> colours = new List<string>
        {
          "Red","Green","Blue","Magenta","Orange","Violet","Indigo","Yellow"
        };

        ObservableCollection<string> myColours = new ObservableCollection<string>();

        public MainPage()
        {
            InitializeComponent();
        }

        private void ColoursSearchBar_SearchButtonPressed(object sender, EventArgs e)
        {
           

        }

        private void ColoursSearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var keyword = ColoursSearchBar.Text;

            if(keyword.Length >= 1)
            {          
            var suggestions = colours.Where(c => c.ToLower().Contains(keyword.ToLower()));

            /*var s = from c in colours.Where(c => c.Contains(keyword))
                    select c;*/

            SuggestionsColourListView.ItemsSource = suggestions;

            SuggestionsColourListView.IsVisible = true;
            }

            else
            {
                SuggestionsColourListView.IsVisible = true;
            }

        }

        private void SuggestionsColourListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var colour = e.Item as string;

            myColours.Add(colour);

            ColourListView.ItemsSource = myColours;

            SuggestionsColourListView.IsVisible = false;

        }
    }
}
