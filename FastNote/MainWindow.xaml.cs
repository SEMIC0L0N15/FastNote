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

        #region MainWindow
        private async void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            await DatabaseAccessor.UpdateLocalDataFromDatabase();
        }

        private void MainWindow_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
                NoteBox.Tag = "15 0 15 50";
            else
                NoteBox.Tag = "15 0 15 10";
        }
        #endregion

        #region Background
        private void Border_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            (sender as FrameworkElement)?.Focus();
            NoteBox.SelectionBehavior.DeselectAndDiscardEditAllItems();
        }

        private void Grid_OnMouseMove(object sender, MouseEventArgs e)
        {
            NoteBox.DragAndDropBehavior.UpdateDraggableTilePosition(e);
        }

        private void Grid_OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            NoteBox.DragAndDropBehavior.EndDragAndDrop();
        } 
        #endregion
    }
}
