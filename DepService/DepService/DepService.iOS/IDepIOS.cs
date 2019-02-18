using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DepService.iOS;
using Foundation;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(IDepIOS))]
namespace DepService.iOS
{
    public class IDepIOS : IDepDemo
    {

        public IDepIOS()
        {

        }

        public string GetPlatformMessage()
        {
            return "This is the IOS version";
        }
    }
}