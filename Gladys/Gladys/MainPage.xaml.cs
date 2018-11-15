using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Gladys
{
	public partial class MainPage : ContentPage
	{
        private bool _popupShow = false;
        public MainPage()
		{
			InitializeComponent();
		}
        public void ShowPanel (object sender, System.EventArgs e)
        {
            // longueur de la popup
            //var width = Application.Current.MainPage.Width * 40 / 100;
            var width = Application.Current.MainPage.Width;
            // position du debut de la popup
            //var x = Application.Current.MainPage.Width-(Application.Current.MainPage.Width*40/100);
            var x = Application.Current.MainPage.Width - (Application.Current.MainPage.Width);
            // Hauteur de l'ecran
            var h = Application.Current.MainPage.Height;
            ShowSLide(x, h, width, h);
            //ShowPanelImg.IsVisible = !ShowPanelImg.IsVisible;
            //HidePanelImg.IsVisible = !HidePanelImg.IsVisible;
            //await Panel.TranslateTo(0, 0, 500, Easing.CubicIn);
        }
        private void ShowSLide(double x, double y, double width, double height)
        {
            if (!_popupShow)
            {
                var rect = new Rectangle(x, 0, width/4, height);
                // affiche la popup a l'endroit et la taille voulu
                Panel.LayoutTo(rect, 450, Easing.CubicOut);
                _popupShow = true;
                //ButtonForShowPanel.Image = "unmore.png";
                //ButtonForShowPanel.IsVisible = false;
            }
            else
            {
                var rect = new Rectangle(Application.Current.MainPage.Width, 0, 0, 0);
                // affichela popup a l'endroit et la taille voulu
                Panel.LayoutTo(rect, 450, Easing.CubicOut);
                //ButtonForShowPanel.Image = "more.png";
                //ButtonForShowPanel.IsVisible = true;
                _popupShow = false;
                //ShowPanelImg.IsVisible = true;
            }
        }
    }
}
