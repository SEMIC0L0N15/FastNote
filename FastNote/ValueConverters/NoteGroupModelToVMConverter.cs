using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FastNote.Core;

namespace FastNote
{
    class NoteGroupModelToVMConverter : BaseValueConverter<NoteGroupModelToVMConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var viewModel = value as NoteGroupViewModel;
            return viewModel?.NoteGroup;
        }
    }
}
