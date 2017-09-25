using System;
using System.Globalization;
using System.Windows;

namespace FastNote
{
    public class BoolToPaddingConverter : BaseValueConverter<BoolToPaddingConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((bool) value) ? new Thickness(8, 20, 8, 20) : new Thickness(8);
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
