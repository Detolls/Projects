using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Saritasa.JiraDayIssues.WPF.Converters
{
    /// <summary>
    /// The class that stores logic to convert stack panel visivility.
    /// </summary>
    public class StackPanelVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// Method to convert value.
        /// </summary>
        /// <returns>Converted value.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value == true ? Visibility.Visible : Visibility.Hidden;
        }

        /// <summary>
        /// Method to back convert value.
        /// </summary>
        /// <returns>Back converted value.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
