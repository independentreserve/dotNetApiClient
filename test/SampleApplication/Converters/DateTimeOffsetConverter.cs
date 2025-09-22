using System;
using System.Globalization;
using System.Windows.Data;

namespace SampleApplication.Converters
{
    /// <summary>
    /// Converter for DateTimeOffset to string for TextBox binding
    /// </summary>
    public class DateTimeOffsetConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTimeOffset dateTimeOffset)
            {
                return dateTimeOffset.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string stringValue && !string.IsNullOrEmpty(stringValue))
            {
                if (DateTimeOffset.TryParse(stringValue, out DateTimeOffset result))
                {
                    return result;
                }
            }
            return null;
        }
    }
}
