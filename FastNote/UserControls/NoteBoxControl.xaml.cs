using System;
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
        private void Item_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            ListBoxItem listBoxItem = GetAssociatedListBoxItem(sender);

            if (IsShiftPressed())
                SetIsMouseDownProperty(listBoxItem, true);

            if (AcceptsClick(listBoxItem))
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                    listBoxItem.IsSelected ^= true;
            }
            else
            {
                DeselectAndDiscardEditAllItems();
                listBoxItem.IsSelected = true;
            }
        }

        private void Item_OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            ListBoxItem listBoxItem = GetAssociatedListBoxItem(sender);
            SetIsMouseDownProperty(listBoxItem, false);
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

                if (IsShiftPressed())
                    SetIsMouseDownProperty(listBoxItem, isMouseDown);

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

        private static bool IsShiftPressed()
        {
            return Keyboard.Modifiers.HasFlag(ModifierKeys.Shift);
        }

        private static void SetIsMouseDownProperty(ListBoxItem listBoxItem, bool value)
        {
            listBoxItem.SetValue(IsMouseDown.ValueProperty, value);
        }

        private bool AcceptsClick(ListBoxItem listBoxItem)
        {
            return ListBox.SelectionMode == SelectionMode.Multiple ||
                   (ListBox.SelectionMode == SelectionMode.Extended && Keyboard.Modifiers.HasFlag(ModifierKeys.Shift)) ||
                   listBoxItem.IsSelected;
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
    }
}
