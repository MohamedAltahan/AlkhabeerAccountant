using System;
using System.Globalization;
using System.Windows.Data;

namespace AlkhabeerAccountant.Helpers.Converters
{
    public class BoolToStatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isActive)
                return isActive ? "نشط" : "غير نشط";

            return "غير محدد";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Optional: allow two-way binding if needed
            if (value is string status)
                return status == "نشط";
            return false;
        }
    }
}
