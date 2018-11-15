using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Gladys
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoaderPage : ContentPage
	{
		public LoaderPage()
		{
			InitializeComponent ();
            Device.StartTimer(TimeSpan.FromSeconds(12), () =>
            {
                Application.Current.MainPage = new Gladys.MainPage();

                return false; // True = Repeat again, False = Stop the timer
            });
        }
	}
}