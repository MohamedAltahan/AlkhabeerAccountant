using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace AlkhabeerAccountant.Helpers.Converters
{
    public class RadioTagMatchConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            MessageBox.Show("تم حفظ البيانات بنجاح!");
            return value?.ToString() == parameter?.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            MessageBox.Show("تم حفظ البيانات بنجاح!");

            if ((bool)value)
                return parameter?.ToString();
            return Binding.DoNothing;
        }
    }
}
