using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uri = Android.Net.Uri;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Telephony;
using Android.Views;
using Android.Widget;
using PhoneWords.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(PhoneDialer))]
namespace PhoneWords.Droid
{
    public class PhoneDialer : IDialer
    {
        public bool Dial(string no)
        {
            var context = MainActivity.Instance;
            if (context == null)
                return false;

            var intent = new Intent(Intent.ActionDial);
            intent.SetData(Uri.Parse("tel:" + no));

            if (IsIntentAvailable(context, intent))
            {
                context.StartActivity(intent);
                return true;
            }

            return false;
        }

        public static bool IsIntentAvailable(Context context, Intent intent)
        {
            var packageManager = context.PackageManager;

            var list = packageManager.QueryIntentServices(intent, 0)
                .Union(packageManager.QueryIntentActivities(intent, 0));

            if (list.Any())
                return true;

            var manager = TelephonyManager.FromContext(context);
            return manager.PhoneType != PhoneType.None;
        }


    }
}