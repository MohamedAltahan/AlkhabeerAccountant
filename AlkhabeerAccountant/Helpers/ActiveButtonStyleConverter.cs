using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace AlkhabeerAccountant.Helpers
{
    public class ActiveButtonStyleConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string activePage && parameter is string buttonPage)
            {
                // If this button's page matches the active page, return active style
                return activePage == buttonPage ?
                    Application.Current.FindResource("GeneralButtonActiveStyle") :
                    Application.Current.FindResource("GeneralButtonStyle");
            }

            return Application.Current.FindResource("GeneralButtonStyle");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
