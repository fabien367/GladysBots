using System;
using System.Globalization;
using Xamarin.Forms;

namespace Gladys.Services.Services
{

    public class UserDialogue : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool)
            {
                var user = (bool)value;

                var assembly = typeof(EmbeddedImage).Assembly;
                if (user)
                    return ImageSource.FromStream(() => assembly.GetManifestResourceStream("Gladys.Services.Images.User.png"));
                else
                    return ImageSource.FromStream(() => assembly.GetManifestResourceStream("Gladys.Services.Images.bot.png"));
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Convert(value, targetType, parameter, culture);
        }
    }
}
