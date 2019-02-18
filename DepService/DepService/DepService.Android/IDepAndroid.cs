using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DepService.Droid;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

[assembly: Xamarin.Forms.Dependency(typeof(IDepAndroid))]
namespace DepService.Droid
{
    public class IDepAndroid : IDepDemo
    {
        public IDepAndroid()
        {

        }

        public string GetPlatformMessage()
        {
            return "This is Android";
        }
    }
}