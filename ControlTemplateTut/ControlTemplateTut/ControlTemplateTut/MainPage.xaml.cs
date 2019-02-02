using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ControlTemplateTut
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            this.ControlTemplate = (ControlTemplate)App.Current.Resources["ThemeMasters"];
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            this.ControlTemplate = (ControlTemplate)App.Current.Resources["ThemeSub"];
        }
    }
}
