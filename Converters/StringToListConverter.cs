using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace Wpf.Frno.SearchAndExtract.Converters
{
    [ValueConversion(typeof(List<string>), typeof(string))]
    public class StringToListConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is IEnumerable<string>)
            {
                return string.Join(", ", (IEnumerable<string>)value);
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string)
            {
                if (((string)value).Contains(","))
                {
                    return ((string)value).Split(',').ToList();
                }
                else if (((string)value).Contains(";"))
                {
                    return ((string)value).Split(';').ToList();
                }
                else
                {
                    return new List<string>() { (string)value };
                }
            }

            return value;
        }
    }
}
