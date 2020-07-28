using Converters;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace EmploymentAgency.Converters
{
    [ValueConversion(typeof(byte[]), typeof(ImageSource))]
    public class ArrayByteToImageSource : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return BitmapConverter.GetImageSource((byte[])value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return BitmapConverter.GetBuffer((ImageSource)value);
        }
    }
}
