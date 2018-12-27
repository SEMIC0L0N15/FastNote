using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace FastNote
{
    public abstract class BaseValueConverter<TConverter> : MarkupExtension, IValueConverter
        where TConverter : class, new()
    {
        private TConverter mConverter;

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return mConverter ?? (mConverter = new TConverter());
        }

        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);
        public abstract object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);            
    }
}
