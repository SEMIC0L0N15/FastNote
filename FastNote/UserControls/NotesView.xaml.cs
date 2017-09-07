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
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Button button = sender as Button;
            ListBoxItem parent = button.TemplatedParent as ListBoxItem;

            parent.IsSelected ^= true;
        }

        private void ListBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ListBox listBox = sender as ListBox;

            foreach (ListBoxItem item in listBox.Items)
            {
                item.IsSelected = false;
            }
        }
    }
}
