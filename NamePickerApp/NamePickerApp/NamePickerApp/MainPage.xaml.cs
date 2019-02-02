using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NamePickerApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            PickerMain.Items.Add("Mark");
            PickerMain.Items.Add("Ari");
            PickerMain.Items.Add("Emmy");
            PickerMain.Items.Add("Camilla");
        }

        private void PickerMain_SelectedIndexChanged(object sender, EventArgs e)
        {
           var name = PickerMain.Items[PickerMain.SelectedIndex];

            DisplayAlert(name, "Selected Value", "OK");
        }
    }
}
