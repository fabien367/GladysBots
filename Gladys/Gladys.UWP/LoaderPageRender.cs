using Gladys;
using Gladys.UWP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Xamarin.Forms.Platform.UWP;
[assembly: ExportRenderer(typeof(LoaderPage), typeof(LoaderPageRender))]
namespace Gladys.UWP
{
   public class LoaderPageRender : PageRenderer
    {
        Page page;
        Application app;
        MediaPlayerElement mediaplayer;
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Page> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || Element == null)
            {
                return;
            }
            app = Application.Current;
            page = new Page();
            var stack = new StackPanel();
            page.Content = stack;
            stack.HorizontalAlignment = HorizontalAlignment.Stretch;
            stack.VerticalAlignment = VerticalAlignment.Stretch;



            var _mediaPlayer = new MediaPlayer();
            _mediaPlayer.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///IntroGladys.mp4"));

            mediaplayer = new MediaPlayerElement();
            mediaplayer.AreTransportControlsEnabled = false;
            mediaplayer.Name = "Player";
            mediaplayer.SetMediaPlayer(_mediaPlayer);
            mediaplayer.HorizontalAlignment = HorizontalAlignment.Stretch;
            mediaplayer.VerticalAlignment = VerticalAlignment.Center;
            mediaplayer.Stretch = Stretch.Uniform;
            stack.Children.Add(mediaplayer);

            this.Children.Add(page);
            mediaplayer.MediaPlayer.Play();
        }
        protected override Size ArrangeOverride(Size finalSize)
        {
            page.Arrange(new Windows.Foundation.Rect(0, 0, finalSize.Width, finalSize.Height));
            mediaplayer.Height = finalSize.Height;
            return finalSize;
        }
    }
}
