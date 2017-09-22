using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using FastNote.Core;
using GalaSoft.MvvmLight;

namespace FastNote
{
    public partial class NoteBoxControl : UserControl
    {
        #region Private Members
        private bool mAutoScroll = true;
        private Point mStartPos = new Point(0,0);
        private Point mRelativeStartPos;
        #endregion

        #region Constructor
        public NoteBoxControl()
        {
            InitializeComponent();
            this.DataContext = ViewModelLocator.GetNoteBoxViewModel();
        }
        #endregion

        #region Initial Actions
        private void NoteBoxControl_OnLoaded(object sender, RoutedEventArgs e)
        {
            TextBox.Focus();
        }
        #endregion

        #region Selecting

        #region Mouse Down / Up
        private void Item_OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            ListBoxItem listBoxItem = GetAssociatedListBoxItem(sender);


            if (AcceptsClick(listBoxItem))
            {
                //if (e.LeftButton == MouseButtonState.Pressed)
                    listBoxItem.IsSelected ^= true;
            }
            else
            {
                DeselectAndDiscardEditAllItems();
                listBoxItem.IsSelected = true;
            }
        }
        #endregion

        #region Mouse Enter / Leave
        private void Presenter_OnMouseEnter(object sender, MouseEventArgs e)
        {
            HandleMouseEnterLeave(sender, e, true);
        }

        private void Presenter_OnMouseLeave(object sender, MouseEventArgs e)
        {
            HandleMouseEnterLeave(sender, e, false);
        }

        private void HandleMouseEnterLeave(object sender, MouseEventArgs e, bool isMouseDown)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                ListBoxItem listBoxItem = GetAssociatedListBoxItem(sender);

                if (AcceptsClick(listBoxItem))
                {
                    listBoxItem.IsSelected = true;
                    e.Handled = true;
                }
            }
        }

        #endregion

        #region Common Helpers
        private static ListBoxItem GetAssociatedListBoxItem(object sender)
        {
            var contentElement = sender as FrameworkElement;
            var listBoxItem = contentElement.TemplatedParent as ListBoxItem;
            return listBoxItem;
        }

        private bool AcceptsClick(ListBoxItem listBoxItem)
        {
            return ListBox.SelectionMode == SelectionMode.Multiple ||
                   (ListBox.SelectionMode == SelectionMode.Extended && IsShiftPressed()) ||
                   listBoxItem.IsSelected;
        }

        private static bool IsShiftPressed()
        {
            return Keyboard.Modifiers.HasFlag(ModifierKeys.Shift);
        }

        private static bool IsControlPressed()
        {
            return Keyboard.Modifiers.HasFlag(ModifierKeys.Control);
        }
        #endregion

        #endregion

        #region Deselecting
        private void Grid_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            DeselectAndDiscardEditAllItems();
        }


        public void DeselectAndDiscardEditAllItems()
        {
            foreach (NoteItemViewModel item in ListBox.Items)
            {
                item.IsSelected = false;
                item.SubmitEdit();
            }
        }
        #endregion

        #region Scrolling
        private void ScrollViewer_OnScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            var scrollViewer = (ScrollViewer)sender;

            if (e.ExtentHeightChange == 0)
            {
                if (scrollViewer.VerticalOffset == scrollViewer.ScrollableHeight)
                    mAutoScroll = true;
                else
                    mAutoScroll = false;
            }

            if (mAutoScroll && e.ExtentHeightChange != 0)
                scrollViewer.ScrollToVerticalOffset(scrollViewer.ExtentHeight);
        }

        private void UIElement_OnPreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var scrollViewer = (ScrollViewer)sender;
            scrollViewer.ScrollToVerticalOffset(scrollViewer.VerticalOffset - e.Delta);
            e.Handled = true;
        }
        #endregion

        private void ListBox_OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            mStartPos = e.GetPosition(BackgroundGrid);
            mRelativeStartPos = e.GetPosition( (IInputElement) e.OriginalSource);
            ViewModelLocator.ApplicationViewModel.IsDragActive = true;
        }

        private void ListBox_OnMouseLeave(object sender, MouseEventArgs e)
        {
            ViewModelLocator.ApplicationViewModel.IsDragActive = false;
        }

        private void ListBox_OnPreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (!ViewModelLocator.ApplicationViewModel.IsDragActive || 
                MovableTextBlock.Visibility == Visibility.Visible || 
                !(e.OriginalSource is TextBlock))
                return;

            Point mousePos = e.GetPosition(BackgroundGrid);
            Vector diff = mStartPos - mousePos;

            if (e.LeftButton == MouseButtonState.Pressed &&
                (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
            {
                MovableTextBlock.Visibility = Visibility.Visible;


                var listBox = sender as ListBox;
                var listBoxItem = FindAnchestor<ListBoxItem>((DependencyObject) e.OriginalSource);

                var noteItemViewModel = listBoxItem.Content as NoteItemViewModel;

                var dataObject = new DataObject("myFormat", noteItemViewModel);

                ViewModelLocator.ApplicationViewModel.CanHighlight = false;
                MovableTextBlock.Text = noteItemViewModel.Content;
                noteItemViewModel.IsSelected = false;

                if (!IsControlPressed())
                    ((NoteBoxViewModel)NoteBox.DataContext).Items.Remove(noteItemViewModel);

                //Task.Run(() => DragDrop.DoDragDrop(listBox, dataObject, DragDropEffects.Move));
            }
        }

        private static T FindAnchestor<T>(DependencyObject current)
            where T : DependencyObject
        {
            do
            {
                if (current is T)
                {
                    return (T)current;
                }
                current = VisualTreeHelper.GetParent(current);
            }
            while (current != null);
            return null;
        }

        public void Grid_OnMouseMove(object sender, MouseEventArgs e)
        {
            //if (MovableTextBlock.Visibility == Visibility.Hidden)
            //    return;

            MovableTextBlock.Margin = new Thickness(e.GetPosition(BackgroundGrid).X - mStartPos.X, e.GetPosition(BackgroundGrid).Y - mRelativeStartPos.Y, 0, 0);
            //MovableTextBlock.UpdateLayout();
        }

        private void ListBox_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MovableTextBlock.Visibility = Visibility.Hidden;
            ViewModelLocator.ApplicationViewModel.IsDragActive = false;
            ViewModelLocator.ApplicationViewModel.CanHighlight = true;
        }
    }
}
