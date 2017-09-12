using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using FastNote.Core;
using GalaSoft.MvvmLight;

namespace FastNote
{
    public partial class NoteBoxControl : UserControl
    {
        #region Private Members
        private bool AutoScroll = true; 
        #endregion

        #region Constructor
        public NoteBoxControl()
        {
            InitializeComponent();
            this.DataContext = ViewModelLocator.NoteBoxViewModel;
        }
        #endregion

        #region Initial Actions
        private void NoteBoxControl_OnLoaded(object sender, RoutedEventArgs e)
        {
            TextBox.Focus();
        }
        #endregion

        #region Selecting
        private void Item_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            ListBoxItem listBoxItem = GetAssociatedListBoxItem(sender);

            if (AcceptsClick(listBoxItem))
            {
                listBoxItem.IsSelected ^= true;
                e.Handled = true;
            }
        }

        private void Presenter_OnMouseEnterLeave(object sender, MouseEventArgs e)
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

        private static ListBoxItem GetAssociatedListBoxItem(object sender)
        {
            var contentElement = sender as FrameworkElement;
            var listBoxItem = contentElement.TemplatedParent as ListBoxItem;
            return listBoxItem;
        }

        private bool AcceptsClick(ListBoxItem listBoxItem)
        {
            return ListBox.SelectionMode == SelectionMode.Multiple ||
                   (ListBox.SelectionMode == SelectionMode.Extended && Keyboard.Modifiers.HasFlag(ModifierKeys.Shift)) ||
                   listBoxItem.IsSelected;
        }
        #endregion

        #region Deselecting
        private void Item_OnLostFocus(object sender, RoutedEventArgs e)
        {
            DeselectAllItems();
        }

        private void UIElement_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            DeselectAllItems();
        }


        public void DeselectAllItems()
        {
            foreach (NoteItemViewModel item in ListBox.Items)
            {
                item.IsSelected = false;
            }
        }
        #endregion

        #region Scrolling
        private void ScrollViewer_OnScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (!(sender is ScrollViewer scrollViewer)) return;

            if (e.ExtentHeightChange == 0)
            {
                if (scrollViewer.VerticalOffset == scrollViewer.ScrollableHeight)
                    AutoScroll = true;
                else
                    AutoScroll = false;
            }

            if (AutoScroll && e.ExtentHeightChange != 0)
                scrollViewer.ScrollToVerticalOffset(scrollViewer.ExtentHeight);
        }

        private void UIElement_OnPreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }
        #endregion
        
    }
}
