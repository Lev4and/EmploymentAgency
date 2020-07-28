using System;
using System.Globalization;
using System.Windows.Data;

namespace EmploymentAgency.Converters
{
    [ValueConversion(typeof(object), typeof(int?))]
    public class SelectedValueToNullableInt32 : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return System.Convert.ToString(value).Length > 0 ? (int?)System.Convert.ToInt32(System.Convert.ToString(value)) : null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (int?)value as object;
        }
    }
}
