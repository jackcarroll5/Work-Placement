using FreshMvvm;
using SearcherApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearcherApp.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SearcherApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TabbingPage : TabbedPage
	{
		public TabbingPage ()
		{
			InitializeComponent ();

          
        }
	}
}