using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.SpeechSynthesis;
using Windows.UI.Xaml.Controls;
using Xamarin.Forms;
using XsAndOs.UWP;

[assembly: Dependency(typeof(TxtToSpeechUWP))]
namespace XsAndOs.UWP
{
    class TxtToSpeechUWP : ITxtToSpeech
    {
        public async void Speak(string txt)
        {
            var mediaProunounce = new MediaElement();
            try
            {
                using (var speech = new SpeechSynthesizer())
                {
                    speech.Voice = SpeechSynthesizer.DefaultVoice;
                    var voiceStreamer = await speech.SynthesizeTextToStreamAsync(txt);
                    mediaProunounce.SetSource(voiceStreamer,voiceStreamer.ContentType);
                    mediaProunounce.Play();
                }
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
        }
    }
}
