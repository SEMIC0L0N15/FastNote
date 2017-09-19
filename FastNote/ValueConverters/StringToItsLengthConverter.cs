using System;
using System.Globalization;
using System.Linq;

namespace FastNote
{
    public class StringToItsLengthConverter : BaseValueConverter<StringToItsLengthConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string text = (string) value;
            return text.Split('\n').OrderByDescending(s => s.Length).ToArray()[0].Length * 8;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
