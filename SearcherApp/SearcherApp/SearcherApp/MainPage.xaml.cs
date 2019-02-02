using System;
using System.Collections.Generic;
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
            "Jake","Jessica","Kate","Laura","Emma","Rachel"
        };


        public MainPage()
        {
            InitializeComponent();

            ListingView.ItemsSource = names;
        }

        private void SearchingBar_SearchButtonPressed(object sender, EventArgs e)
        {
            string keyword = SearchingBar.Text;

            IEnumerable<string> searchRes = names.Where(name => name.ToLower().Contains(keyword.ToLower()));
            IEnumerable<string> searchRes1 = from name
                                             in names
                                             where name.Contains(keyword)
                                             select name;


            ListingView.ItemsSource = searchRes;
        }
    }
}
