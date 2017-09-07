using FastNote.Core;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FastNote
{
    /// <summary>
    /// Interaction logic for NotesView.xaml
    /// </summary>
    public partial class NotesView : UserControl
    {
        public NotesView()
        {
            InitializeComponent();

            this.DataContext = new NotesViewViewModel();
        }

        private void TextBlock_MouseDown(object sender, System.Windows.RoutedEventArgs e)
        {
            FrameworkElement element = sender as FrameworkElement;
            ListBoxItem parent = element.TemplatedParent as ListBoxItem;

            parent.IsSelected ^= true;

            e.Handled = true;
        }

        private void ListBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ListBox listBox = sender as ListBox;

            foreach (NoteItemViewModel item in listBox.Items)
            {
                item.IsSelected = false;
            }
        }
    }
}
