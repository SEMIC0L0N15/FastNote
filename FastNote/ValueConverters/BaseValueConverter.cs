using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace FastNote
{
    public abstract class BaseValueConverter<TConverter> : MarkupExtension, IValueConverter
        where TConverter : class, new()
    {
        #region Private Members
        private TConverter mConverter;
        #endregion

        #region MarkupExtension Methods
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return mConverter ?? (mConverter = new TConverter());
        }
        #endregion

        #region IValueConverter Methods
        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);
        public abstract object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);            
        #endregion
    }
}
