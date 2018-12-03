using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace GUI
{
    [ValueConversion(typeof(string), typeof(BitmapImage))]
    public class PathToIconConverter : IValueConverter
    {
        public static readonly PathToIconConverter Instance = new PathToIconConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string iconPath = (string)value;

            if (iconPath == null)
            {
                iconPath = "Icons/Enumerator.png";
            }

            return new BitmapImage(new Uri($"pack://application:,,,/" + iconPath));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
