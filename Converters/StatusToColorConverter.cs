using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace Logistics_Software.Converters
{
    class StatusToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string status = value?.ToString()?.ToLower() ?? "";

            return status switch
            {
                "выполнен" or "доставлен" => new SolidColorBrush(Color.FromRgb(76, 175, 80)),   // #4CAF50
                "в процессе" => new SolidColorBrush(Color.FromRgb(255, 107, 53)),               // #FF6B35
                "отменен" or "ошибка" => new SolidColorBrush(Color.FromRgb(255, 68, 68)),       // #FF4444
                _ => new SolidColorBrush(Colors.Gray)
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => null;
    }
}
