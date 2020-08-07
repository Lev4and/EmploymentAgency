using System;
using System.Globalization;
using System.Windows.Data;

namespace EmploymentAgency.Converters
{
    [ValueConversion(typeof(DateTime?), typeof(string))]
    public class DateTimeToStringDateTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((DateTime?)value != null ? ((DateTime)value).ToString("dd-MM-yyyy HH:mm:ss") : "Неизвестно");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
