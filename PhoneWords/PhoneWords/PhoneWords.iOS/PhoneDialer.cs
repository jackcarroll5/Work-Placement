using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using PhoneWords.iOS;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(PhoneDialer))]
namespace PhoneWords.iOS
{
   public class PhoneDialer : IDialer
    {
        public bool Dial(string no)
        {
            return UIApplication.SharedApplication.OpenUrl(new NSUrl("Tel:" + no));

        }
    }
}