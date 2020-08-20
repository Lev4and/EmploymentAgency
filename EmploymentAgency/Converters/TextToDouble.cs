using System;
using System.Globalization;
using System.Windows.Data;

namespace EmploymentAgency.Converters
{
    [ValueConversion(typeof(string), typeof(double))]
    public class TextToDouble : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return double.Parse(System.Convert.ToString(value));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return System.Convert.ToString(value);
        }
    }
}
