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
        private bool autoScroll = true;
        private Point startPosition;
        #endregion

        #region Public Properties
        public NoteBoxViewModel ViewModel => (NoteBoxViewModel) DataContext;
        #endregion

        #region Constructor
        public NoteBoxControl()
        {
            InitializeComponent();
            DataContext = ViewModelLocator.GetNoteBoxViewModel();
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
        private void ListBoxItem_OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            HandleMouseUp((ListBoxItem) sender, e);
        }

        private void HandleMouseUp(ListBoxItem listBoxItem, MouseButtonEventArgs e)
        {
            if (AcceptsClick(listBoxItem))
            {
                if (e.ChangedButton == MouseButton.Left)
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
        private void ListBoxItem_OnMouseEnter(object sender, MouseEventArgs e)
        {
            HandleMouseEnterLeave((ListBoxItem) sender, e);
        }

        private void ListBoxItem_OnMouseLeave(object sender, MouseEventArgs e)
        {
            HandleMouseEnterLeave((ListBoxItem) sender, e);
        }

        private void HandleMouseEnterLeave(ListBoxItem listBoxItem, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (AcceptsClick(listBoxItem))
                {
                    listBoxItem.IsSelected = true;
                    e.Handled = true;
                }
            }
        }

        #endregion

        #region Common Helpers
        private static ListBoxItem GetAssociatedListBoxItem(FrameworkElement contentElement)
        {
            var listBoxItem = contentElement.TemplatedParent as ListBoxItem;
            return listBoxItem;
        }

        private bool AcceptsClick(ListBoxItem listBoxItem)
        {
            NoteItemViewModel viewModel = GetNoteItemViewModelFrom(listBoxItem);
            bool isBeingEdited = viewModel?.IsBeingEdited ?? false;

            return ListBox.SelectionMode == SelectionMode.Multiple ||
                   (ListBox.SelectionMode == SelectionMode.Extended && IsShiftPressed()) ||
                   listBoxItem.IsSelected ||
                   isBeingEdited;
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
        private void Background_OnMouseDown(object sender, MouseButtonEventArgs e)
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
                    autoScroll = true;
                else
                    autoScroll = false;
            }

            if (autoScroll && e.ExtentHeightChange != 0)
                scrollViewer.ScrollToVerticalOffset(scrollViewer.ExtentHeight);
        }

        private void UIElement_OnPreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var scrollViewer = (ScrollViewer)sender;
            scrollViewer.ScrollToVerticalOffset(scrollViewer.VerticalOffset - e.Delta);
            e.Handled = true;
        }
        #endregion

        #region Drag and Drop

        #region Event Handlers
        private void ListBoxItem_OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            InitDragAndDrop((ListBoxItem) sender, e);
        }

        public void ListBox_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            EndDragAndDrop();
        }

        public void Grid_OnMouseMove(object sender, MouseEventArgs e)
        {
            UpdateDraggableTilePosition(e);
        }

        private void ListBoxItem_OnPreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (e.OriginalSource is TextBlock)
            {
                StartDragAndDropIfPossible((ListBoxItem) sender, e);
            }
        }
        #endregion

        #region Init/End DragAndDrop
        private void InitDragAndDrop(ListBoxItem listBoxItem, MouseButtonEventArgs e)
        {
            startPosition = e.GetPosition((IInputElement)e.OriginalSource);

            NoteItemViewModel noteItemViewModel = GetNoteItemViewModelFrom(listBoxItem);
            NoteItem noteItem = noteItemViewModel.NoteItem;

            ViewModelLocator.ApplicationViewModel.DraggingObject = noteItem;
        }

        private void EndDragAndDrop()
        {
            HideDraggableTile();
            ViewModelLocator.ApplicationViewModel.DraggingObject = null;
        } 
        #endregion

        #region DraggableTile
        private void UpdateDraggableTilePosition(MouseEventArgs e)
        {
            DraggableTile.Margin = new Thickness(
                e.GetPosition(BackgroundGrid).X - startPosition.X,
                e.GetPosition(BackgroundGrid).Y - startPosition.Y, 0, 0);
        }

        private void ShowDraggableTile()
        {
            DraggableTile.Visibility = Visibility.Visible;
        }

        private void HideDraggableTile()
        {
            DraggableTile.Visibility = Visibility.Hidden;
        } 
        #endregion

        #region StartDragAndDropIfPossible
        private void StartDragAndDropIfPossible(ListBoxItem listBoxItem, MouseEventArgs e)
        {
            if (ShouldStartDragAndDrop() && DraggedEnoughDistance(e))
            {
                ShowDraggableTile();

                NoteItemViewModel noteItemViewModel = GetNoteItemViewModelFrom(listBoxItem);
                StartDragAndDrop(noteItemViewModel);
            }
        }

        private bool ShouldStartDragAndDrop()
        {
            return ViewModelLocator.ApplicationViewModel.IsDragActive &&
                   DraggableTile.Visibility != Visibility.Visible;
        }

        private bool DraggedEnoughDistance(MouseEventArgs e)
        {
            Point mousePos = e.GetPosition((IInputElement)e.OriginalSource);
            Vector distance = startPosition - mousePos;

            return (Math.Abs(distance.X) > SystemParameters.MinimumHorizontalDragDistance ||
                    Math.Abs(distance.Y) > SystemParameters.MinimumVerticalDragDistance) &&
                    LeftMouseButtonPressed(e);
        }

        private bool LeftMouseButtonPressed(MouseEventArgs e)
        {
            return e.LeftButton == MouseButtonState.Pressed;
        }

        private void StartDragAndDrop(NoteItemViewModel noteItemViewModel)
        {
            DraggableTile.Text = noteItemViewModel.Content;
            noteItemViewModel.IsSelected = false;

            if (!IsControlPressed())
                ((NoteBoxViewModel)NoteBox.DataContext).DeleteNote(noteItemViewModel);
        }
        #endregion

        #endregion

        #region NoteItemViewModel Getters
        private static NoteItemViewModel GetNoteItemViewModelFrom(ListBoxItem listBoxItem)
        {
            return listBoxItem.DataContext as NoteItemViewModel;
        }
        #endregion
    }
}
