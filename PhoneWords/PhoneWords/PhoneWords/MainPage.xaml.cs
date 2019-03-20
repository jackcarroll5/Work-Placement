using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PhoneWords
{
    public partial class MainPage : ContentPage
    {
        string translatedNo;

        public MainPage()
        {
            InitializeComponent();
        }

        void OnTranslate(object sender, EventArgs e)
        {
            translatedNo = PhoneTranslator.ToNo(phoneNumberText.Text);
            if (!string.IsNullOrWhiteSpace(translatedNo))
            {
                callButton.IsEnabled = true;
                callButton.Text = "Call " + translatedNo;
            }
            else
            {
                callButton.IsEnabled = false;
                callButton.Text = "Call";
            }
        }

        async void OnCall(object sender, EventArgs e)
        {
            if (await this.DisplayAlert(
                    "Dial a Number",
                    "Would you like to call " + translatedNo + "?",
                    "Yes",
                    "No"))
            {
                var dialer = DependencyService.Get<IDialer>();
                if (dialer != null)
                    App.PhoneNumbers.Add(translatedNo);
                    callHistoryButton.IsEnabled = true;
                    dialer.Dial(translatedNo);
            }
        }


        async void OnCallHistory(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CallHistoryPage());
        }

      }
}
