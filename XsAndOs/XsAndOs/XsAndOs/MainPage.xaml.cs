using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XsAndOs
{
    public partial class MainPage : ContentPage
    {

        private Button[] buttons = new Button[9];
        private XAndOEngine game = new XAndOEngine();

     

        public MainPage()
        {
            InitializeComponent();

            buttons[0] = button1;
            buttons[1] = button2;
            buttons[2] = button3;
            buttons[3] = button4;
            buttons[4] = button5;
            buttons[5] = button6;
            buttons[6] = button7;
            buttons[7] = button8;
            buttons[8] = button9;
        }

        private void Button1_Clicked(object sender, EventArgs e)
        {
            game.SetButton((Button)sender);
            if(game.checkWinner(buttons))
            {
                ITxtToSpeech speech = DependencyService.Get<ITxtToSpeech>();

                if(speech != null)
                {
                    speech.Speak("We're in the endgame now!");
                }

                GameOverStackLayout.IsVisible = true;
            }
        }

        private void Button_ClickedPlayAgain(object sender, EventArgs e)
        {
            game.gameReset(buttons);
            GameOverStackLayout.IsVisible = false;
        }
    }
}
