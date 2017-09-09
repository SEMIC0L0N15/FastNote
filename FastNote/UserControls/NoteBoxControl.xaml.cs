using FastNote.Core;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FastNote
{
    /// <summary>
    /// Interaction logic for NotesView.xaml
    /// </summary>
    public partial class NoteBoxControl : UserControl
    {
        public NoteBoxControl()
        {
            InitializeComponent();

            //this.DataContext = new NotesViewViewModel();
        }

        private void ListBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            /*ListBox listBox = sender as ListBox;
            listBox.Focus();*/
        }

        private void Item_MouseDown(object sender, RoutedEventArgs e)
        {
            FrameworkElement element = sender as FrameworkElement;
            ListBoxItem parent = element.TemplatedParent as ListBoxItem;

            parent.IsSelected ^= true;
            //element.Focus();

            e.Handled = true;
        }

        private void Item_LostFocus(object sender, RoutedEventArgs e)
        {
            foreach (NoteItemViewModel item in this.ListBox.Items)
            {
                item.IsSelected = false;
            }
        }
    }
}
