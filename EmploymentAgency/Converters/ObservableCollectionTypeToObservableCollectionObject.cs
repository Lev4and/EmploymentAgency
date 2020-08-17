using Converters;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace EmploymentAgency.Converters
{
    [ValueConversion(typeof(ObservableCollection<Type>), typeof(ObservableCollection<object>))]
    public class ObservableCollectionTypeToObservableCollectionObject : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                return CollectionConverter<object>.ConvertToObservableCollection(CollectionConverter<Type>.ConvertToObjectList(((ObservableCollection<Type>)value).ToList()));
            }
            else
                return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
