using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using SampleApplication.ViewModels;

namespace SampleApplication.Converters
{
    /// <summary>
    /// This converter used to show/hide method parameters on the UI depending on selected method to call
    /// </summary>
    public class MethodParameterToVisibilityConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is MethodMetadata)
            {
                var method = (MethodMetadata) value;

                string paramterName = parameter != null ? parameter.ToString() : string.Empty;

                if (string.IsNullOrWhiteSpace(paramterName))
                {
                    return
                    method.Parameters.Any()
                        ? Visibility.Visible
                        : Visibility.Collapsed;
                }

                return
                    method.Parameters.Any(
                        p => string.Compare(p, paramterName, StringComparison.InvariantCultureIgnoreCase) == 0)
                        ? Visibility.Visible
                        : Visibility.Collapsed;

            }

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
