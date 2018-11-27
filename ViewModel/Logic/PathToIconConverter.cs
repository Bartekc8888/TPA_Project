using System;
using System.Globalization;
using System.Reflection;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using log4net;

namespace ViewModel.Logic
{
    [ValueConversion(typeof(string), typeof(BitmapImage))]
    public class PathToIconConverter : IValueConverter
    {
        private static readonly ILog Log = LogManager.GetLogger
               (MethodBase.GetCurrentMethod().DeclaringType);

        public static readonly PathToIconConverter Instance = new PathToIconConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Log.Debug("Converting icon's string to image");

            string iconPath = (string)value;

            if (iconPath == null)
            {
                iconPath = "Icons/Enumerator.png";
            }

            return new BitmapImage(new Uri(@"ViewModel_Assembly;;;component/" + iconPath, UriKind.Relative));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Log.Error("Error when try to convert back");

            throw new NotImplementedException();
        }
    }
}
