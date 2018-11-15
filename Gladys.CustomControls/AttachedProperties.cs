using System;
using Xamarin.Forms;

namespace Gladys.CustomControls
{
    public static class AttachedProperties
    {
        public static readonly BindableProperty EffectVisibilityProperty =
   BindableProperty.CreateAttached("EffectVisibility", typeof(bool), typeof(AttachedProperties), false, propertyChanging: (a, b, c) => SlideAnim(a, b, c));

        public static readonly BindableProperty RotationProperty =
   BindableProperty.CreateAttached("Rotation", typeof(bool), typeof(AttachedProperties), false, propertyChanging: (a, b, c) => RotationAnim(a, b, c));

        private async static void RotationAnim(BindableObject a, object b, object c)
        {
            var retour = (Entry)a;
            if ((bool)c)
            {
              await  retour.RotateXTo(251 * 360);
            }
            else
                await retour.RotateXTo(-251 * 360);
        }

        private async static void SlideAnim(BindableObject a, object b, object c)
        {
            var retour = (Entry)a;
            if ((bool)c)
            {
                await retour.ScaleTo(1);
            }
            else
                await retour.ScaleTo(0);
        }

        public static bool GetEffectVisibility(BindableObject view)
        {
            return (bool)view.GetValue(EffectVisibilityProperty);
        }

        public static void SetEffectVisibility(BindableObject view, bool value)
        {
            view.SetValue(EffectVisibilityProperty, value);
        }
        public static bool GetRotation(BindableObject view)
        {
            return (bool)view.GetValue(RotationProperty);
        }

        public static void SetRotation(BindableObject view, bool value)
        {
            view.SetValue(RotationProperty, value);
        }
    }
}
