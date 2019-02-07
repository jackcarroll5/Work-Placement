using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Speech.Tts;
using Android.Views;
using Android.Widget;
using XsAndOs.Droid;


[assembly: Dependency(typeof(TxtToSpeechAndroid))]
namespace XsAndOs.Droid
{
   public class TxtToSpeechAndroid : Java.Lang.Object, ITxtToSpeech, TextToSpeech.IOnInitListener
    {
        TextToSpeech speech;
        string lastTxt;

        public void Speak(string txt)
        {
            if(speech == null)
            {
                lastTxt = txt;
                speech = new TextToSpeech(Android.App.Application.Context,this);
            }
            else
            {
                if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
                    speech.Speak(txt, QueueMode.Flush, null, null);
                else
                {
#pragma warning disable CS0618 // Type or member is obsolete
                    speech.Speak(txt, QueueMode.Flush, null);
#pragma warning restore CS0618 // Type or member is obsolete
                }              
            }
        }

        public void OnInit(OperationResult status)
        {
            if(status == OperationResult.Success)
            {
                if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
                    speech.Speak(lastTxt, QueueMode.Flush, null, null);
                else
                {
#pragma warning disable CS0618 // Type or member is obsolete
                    speech.Speak(lastTxt, QueueMode.Flush, null);
#pragma warning restore CS0618 // Type or member is obsolete
                }
                lastTxt = null;
            }
        }

    }
}