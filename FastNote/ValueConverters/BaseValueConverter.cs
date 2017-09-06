using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace FastNote
{
    public abstract class BaseValueConverter<ConverterType> : MarkupExtension, IValueConverter
        where ConverterType : class, new()
    {
        #region Private Members
        private ConverterType mConverter;
        #endregion

        #region MarkupExtension Methods
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return mConverter ?? (mConverter = new ConverterType());
        }
        #endregion

        #region IValueConverter Methods
        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);
        public abstract object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);            
        #endregion
    }
}
