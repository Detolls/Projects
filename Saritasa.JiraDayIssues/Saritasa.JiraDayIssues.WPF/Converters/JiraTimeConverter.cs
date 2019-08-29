using Saritasa.JiraDayIssues.Domain.Issues.Builders;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Saritasa.JiraDayIssues.WPF.Converters
{
    /// <summary>
    /// The class that stores logic to convert time into a specific time for JIRA.
    /// </summary>
    public class JiraTimeConverter : IValueConverter
    {
        /// <summary>
        /// Method to convert value.
        /// </summary>
        /// <returns>Converted value.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return JiraTimeBuilder.BuildStringTime((int)value);
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
