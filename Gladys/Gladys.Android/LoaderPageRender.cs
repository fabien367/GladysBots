using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Gladys;
using Gladys.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(LoaderPage), typeof(LoaderPageRender))]
namespace Gladys.Droid
{
    public class LoaderPageRender : PageRenderer
    {
        Activity activity;
        TextureView textureView;
        Android.Views.View _view;

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Page> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || Element == null)
            {
                return;
            }


            var layout = new Android.Widget.LinearLayout(this.Context);
            layout.SetBackgroundColor(Android.Graphics.Color.Black);
            layout.LayoutParameters = new LayoutParams(LayoutParams.MatchParent, LayoutParams.MatchParent);
            VideoView mVideoView = new VideoView(Context);
            mVideoView.SetVideoPath("android.resource://" + this.Context.PackageName + "/" + Resource.Drawable.IntroGladys);
            layout.SetVerticalGravity(GravityFlags.Center);
            layout.SetHorizontalGravity(GravityFlags.Center);
            mVideoView.LayoutParameters = new LayoutParams(LayoutParams.FillParent, LayoutParams.FillParent);
            layout.AddView(mVideoView);
            mVideoView.Start();
            _view = layout;
            AddView(_view);
        }

        protected override void OnLayout(bool changed, int left, int top, int right, int bottom)
        {
            //base.OnLayout(changed, left, top, right, bottom);
            var msw = MeasureSpec.MakeMeasureSpec(right - 1, MeasureSpecMode.Exactly);
            var msh = MeasureSpec.MakeMeasureSpec(bottom - 1, MeasureSpecMode.Exactly);
            //_view.Measure(right, bottom);
            _view.Measure(msw, msh);
            _view.Layout(0, 0, right, bottom - top);
        }
    }
}