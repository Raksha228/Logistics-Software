using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Logistics_Software.Converters
{
    class RoleToVisibilityConverter : IValueConverter
    {
        public string TargetRole { get; set; } = "Administrator";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string currentRole = value?.ToString() ?? "";
            return currentRole.Equals(TargetRole, StringComparison.OrdinalIgnoreCase)
                ? Visibility.Visible
                : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => null;
    }
}
