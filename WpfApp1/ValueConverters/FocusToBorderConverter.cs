using System;
using System.Globalization;
using System.Windows;

namespace FastNote
{
    class FocusToBorderConverter : BaseValueConverter<FocusToBorderConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? Application.Current.Resources["AzureDarkBrush"] : Application.Current.Resources["BackgroundVeryDarkBrush"];
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
