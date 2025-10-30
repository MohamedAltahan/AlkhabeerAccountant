using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace AlkhabeerAccountant.Helpers.Converters
{
    /*
     we use the converter to connect these different types logically.

     The RadioButton’s IsChecked (a bool)
     Your ViewModel’s CostMethod (or any property) (a string)
     */
    public class RadioTagMatchConverter : IValueConverter
    {
        //'value' comes from the binding source (the ViewModel property)
        //'parameter' comes from the RadioButton's Tag(view)

        //working when view model property ---> set the radio button state
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value?.ToString() == parameter?.ToString();
        }

        //working when  radio button state ---> set the view model property
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
                return parameter?.ToString();
            return Binding.DoNothing;
        }
    }
}
