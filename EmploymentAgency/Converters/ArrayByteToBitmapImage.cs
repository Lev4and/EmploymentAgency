using Converters;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace EmploymentAgency.Converters
{
    [ValueConversion(typeof(byte[]), typeof(BitmapImage))]
    public class ArrayByteToBitmapImage : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return BitmapConverter.GetBitmapImage((byte[])value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return BitmapConverter.GetBuffer((BitmapImage)value);
        }
    }
}
