using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using FastNote.Core;
using GalaSoft.MvvmLight;

namespace FastNote
{
    public partial class NoteBoxControl : UserControl
    {
        private bool AutoScroll = true;

        public NoteBoxControl()
        {
            InitializeComponent();
            this.DataContext = ViewModelLocator.NoteBoxViewModel;
        }

        public void DeselectAllItems()
        {
            foreach (NoteItemViewModel item in ListBox.Items)
            {
                item.IsSelected = false;
            }
        }

        private void Item_OnLostFocus(object sender, RoutedEventArgs e)
        {
            DeselectAllItems();
        }

        private void UIElement_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            DeselectAllItems();
        }

        private void Item_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is FrameworkElement contentElement)
            {
                if (contentElement.TemplatedParent is ListBoxItem listBoxItem)
                {
                    if (ListBox.SelectionMode == SelectionMode.Multiple ||
                        (ListBox.SelectionMode == SelectionMode.Extended && Keyboard.Modifiers.HasFlag(ModifierKeys.Shift)) ||
                        listBoxItem.IsSelected)
                    {
                        listBoxItem.IsSelected ^= true;
                        e.Handled = true;
                    }
                }
            }
        }

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

        private void NoteBoxControl_OnLoaded(object sender, RoutedEventArgs e)
        {
            TextBox.Focus();
        }

        private void Presenter_OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (sender is FrameworkElement contentElement)
                {
                    if (contentElement.TemplatedParent is ListBoxItem listBoxItem)
                    {
                        if (ListBox.SelectionMode == SelectionMode.Multiple ||
                            (ListBox.SelectionMode == SelectionMode.Extended && Keyboard.Modifiers.HasFlag(ModifierKeys.Shift)) ||
                            listBoxItem.IsSelected)
                        {
                            listBoxItem.IsSelected = true;
                            e.Handled = true;
                        }
                    }
                }
            }
        }
    }
}
