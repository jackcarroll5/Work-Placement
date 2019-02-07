using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AVFoundation;
using Foundation;
using XsAndOs.iOS;
using Xamarin.Forms;
using UIKit;

[assembly: Dependency(typeof(TxtToSpeechIOS))]
namespace XsAndOs.iOS
{
    public class TxtToSpeechIOS : ITxtToSpeech
    {
        public void Speak(string txt)
        {
            var speechSynth = new AVSpeechSynthesizer();

            speechSynth.SpeakUtterance(new AVSpeechUtterance(txt)
            {
                Rate = AVSpeechUtterance.DefaultSpeechRate,
                Voice = AVSpeechSynthesisVoice.FromLanguage("en-US"),
                Volume = 0.5f,
                PitchMultiplier = 1.0f
            });
        }
    }
}