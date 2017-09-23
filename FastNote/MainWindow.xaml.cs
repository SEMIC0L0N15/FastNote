using FastNote.Core;
using System.Windows;
using System.Windows.Input;
using FastNote.Core.Database;

namespace FastNote
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = new MainWindowViewModel(this);
        }

        private void Border_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            (sender as FrameworkElement)?.Focus();
            NoteBox.DeselectAndDiscardEditAllItems();
        }

        private void MainWindow_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
                NoteBox.Tag = "15 0 15 50";
            else
                NoteBox.Tag = "15 0 15 10";
        }

        private async void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            await DatabaseAccessor.UpdateLocalDataFromDatabase();
        }

        private void Grid_OnMouseMove(object sender, MouseEventArgs e)
        {
            NoteBox.Grid_OnMouseMove(sender, e);
        }

        private void Grid_OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            NoteBox.ListBox_OnMouseLeftButtonUp(sender, e);
        }
    }
}
