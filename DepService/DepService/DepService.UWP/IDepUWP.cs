using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DepService.UWP;


[assembly: Xamarin.Forms.Dependency(typeof(IDepUWP))]
namespace DepService.UWP
{
    public class IDepUWP : IDepDemo
    {
        public IDepUWP()
        {

        }

        public string GetPlatformMessage()
        {
            return "This is UWP";
        }
    }
}
