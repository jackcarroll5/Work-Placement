using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SearcherApp
{
    public partial class MainPage : ContentPage
    {
        private readonly List<string> names = new List<string>
        {
            "Jake","Jessica","Kate","Laura","Emma","Rachel","Ramona","Scarlett","Brie",
            "Robert","Chris","Mark","Tom","Paul","Emily","Clodagh","Elizabeth","Josh"
        };

        ObservableCollection<string> myNames = new ObservableCollection<string>();


        public MainPage()
        {
            InitializeComponent();

            //.SetHasNavigationBar(this, false);

            NameSuggestionListView.ItemsSource = names;
        }

        private void SearchingBar_SearchButtonPressed(object sender, EventArgs e)
        {
            string keyword = SearchingBar.Text;

            if (keyword.Length >= 1)
            {

                IEnumerable<string> searchRes = names.Where(name => name.ToLower().Contains(keyword.ToLower()));
                /*IEnumerable<string> searchRes1 = from name
                                                 in names
                                                 where name.Contains(keyword)
                                                 select name;*/


                NameSuggestionListView.ItemsSource = searchRes;
            }
            else
            {
                NameSuggestionListView.ItemsSource = new List<string>() { "Name not Found" };
            }
        }

        private void SearchingBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.NewTextValue.ToString()))
                NameSuggestionListView.ItemsSource = names;
            else
            {
                string keyword = SearchingBar.Text;

                if (keyword.Length >= 1)
                {

                    IEnumerable<string> searchRes = names.Where(name => name.ToLower().Contains(keyword.ToLower()));
                    /*IEnumerable<string> searchRes1 = from name
                                                     in names
                                                     where name.Contains(keyword)
                                                     select name;*/

                    NameSuggestionListView.ItemsSource = searchRes;

                    NameSuggestionListView.IsVisible = true;

                }

                else
                {
                    NameSuggestionListView.ItemsSource = new List<string>() { "Name not Found" };
                    // NameSuggestionListView.IsVisible = false;

                }
            }
        }

        private void NameSuggestionListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var name = e.Item as string;

            myNames.Add(name);

            //NameSuggestionListView.ItemsSource = name.Where(c => c.Equals(name));
            NameSuggestionListView.ItemsSource = myNames;

            NameSuggestionListView.IsVisible = true;

            //SearchingBar.Text = name;

            DisplayAlert("Names List Updated", name + " is now added to the list of names", "OK");
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();

        }

        private void NextPageButton_Clicked(object sender, EventArgs e)
        {
            var page = new NewPage();

            NavigationPage.SetBackButtonTitle(page, "MainPageBack");

            //NavigationPage.SetHasBackButton(page, true);

            Navigation.PushAsync(page);
        }


        //Clearing the entire list of names chosen after searching for their names
        private void Remove_Clicked(object sender, EventArgs e)
        {
            myNames.Clear();
        }


        //Picking a date with DatePicker for testing purposes and labelling today's date for example
        private void DateChooser_DateSelected(object sender, DateChangedEventArgs e)
        {
            DateLabel.Text = "The chosen date is " + e.NewDate.ToLongDateString();
        }
    }
}
