﻿using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace GUI.Logic
{
    [ValueConversion(typeof(string), typeof(BitmapImage))]
    public class PathToIconConverter : IValueConverter
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
               (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static PathToIconConverter Instance = new PathToIconConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            log.Debug("Converting icon's string to image");

            string iconPath = (string)value;

            if (iconPath == null)
            {
                iconPath = "Icons/Enumerator.png";
            }

            return new BitmapImage(new Uri($"pack://application:,,,/" + iconPath));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            log.Error("Error when try to convert back");

            throw new NotImplementedException();
        }
    }
}