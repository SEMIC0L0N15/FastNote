using FastNote.Core;
using System.Windows.Controls;

namespace FastNote
{
    /// <summary>
    /// Interaction logic for NoteCategoryList.xaml
    /// </summary>
    public partial class NoteGroupListControl : UserControl
    {
        public NoteGroupListControl()
        {
            InitializeComponent();

            this.DataContext = IoC.Get<NoteGroupListViewModel>();
        }
    }
}
