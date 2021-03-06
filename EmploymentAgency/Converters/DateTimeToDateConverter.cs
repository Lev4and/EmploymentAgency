﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace EmploymentAgency.Converters
{
    [ValueConversion(typeof(DateTime?), typeof(string))]
    public class DateTimeToDateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((DateTime?)value != null ? ((DateTime)value).ToString("dd-MM-yyyy") : "Неизвестно");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
