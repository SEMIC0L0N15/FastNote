using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using FastNote.Core;

namespace FastNote
{
    #region Property
    public class RichSelectionBehaviorProperty
        : AttachedBehaviorProperty<RichSelectionBehaviorProperty, FrameworkElement>
    {
        protected override AttachedBehavior<FrameworkElement> CreateAttachedBehavior(DependencyObject d)
        {
            return new RichSelectionBehavior((FrameworkElement) d);
        }
    }
    #endregion

    public class RichSelectionBehavior : AttachedBehavior<FrameworkElement>
    {
        #region Private Members
        private IRichItemsControl RichItemsControl { get; set; }
        private FrameworkElement Background => RichItemsControl.GetBackground();
        #endregion

        #region Contructor
        public RichSelectionBehavior(FrameworkElement associatedObject)
            : base(associatedObject)
        {
        }
        #endregion

        #region Attach/Detach
        public override void OnAttached()
        {
            if (AssociatedObject is IRichItemsControl richItemsControl)
            {
                RichItemsControl = richItemsControl;

                RichItemsControl.ItemsChanged += () =>
                {
                    foreach (FrameworkElement item in RichItemsControl.GetItems())
                    {
                        item.MouseUp -= Item_OnMouseUp;
                        item.MouseEnter -= Item_OnMouseEnter;
                        item.MouseLeave -= Item_OnMouseLeave;

                        item.MouseUp += Item_OnMouseUp;
                        item.MouseEnter += Item_OnMouseEnter;
                        item.MouseLeave += Item_OnMouseLeave;
                    }
                };

                Background.MouseDown += Background_OnMouseDown;
                Background.KeyDown += Background_OnKeyDown;
            }
            else
                throw new ArgumentException("This behavior could be attached only to objects of type IRichItemsControl");
        }

        public override void OnDetaching()
        {
            if (AssociatedObject is IRichItemsControl)
            {
                RichItemsControl.ItemsChanged += () =>
                {
                    foreach (FrameworkElement item in RichItemsControl.GetItems())
                    {
                        item.MouseUp -= Item_OnMouseUp;
                        item.MouseEnter -= Item_OnMouseEnter;
                        item.MouseLeave -= Item_OnMouseLeave;
                    }
                };

                Background.MouseDown -= Background_OnMouseDown;
                Background.KeyDown -= Background_OnKeyDown;
            }
        }
        #endregion

        #region Selecting

        #region Mouse Up
        private void Item_OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            HandleMouseUp((ListBoxItem)sender, e);
        }

        public void HandleMouseUp(ListBoxItem listBoxItem, MouseButtonEventArgs e)
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

            if (listBoxItem.DataContext is NoteItemViewModel noteItemViewModel &&
                !noteItemViewModel.IsBeingEdited)
            {
                Background.Focus();
            }
        }
        #endregion

        #region Mouse Enter / Leave
        private void Item_OnMouseEnter(object sender, MouseEventArgs e)
        {
            HandleMouseEnterLeave((ListBoxItem)sender, e);
        }

        private void Item_OnMouseLeave(object sender, MouseEventArgs e)
        {
            HandleMouseEnterLeave((ListBoxItem)sender, e);
        }

        public void HandleMouseEnterLeave(ListBoxItem listBoxItem, MouseEventArgs e)
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
        private bool AcceptsClick(ListBoxItem listBoxItem)
        {
            NoteItemViewModel viewModel = GetNoteItemViewModelFrom(listBoxItem);
            bool isBeingEdited = viewModel?.IsBeingEdited ?? false;

            return IsShiftPressed() ||
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
            foreach (NoteItemViewModel item in RichItemsControl.GetItemsControl().Items)
            {
                item.IsSelected = false;
                item.SubmitEdit();
            }
        }
        #endregion

        #region Deleting
        private void Background_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                DeleteSelectedItems();
            }
        }

        public void DeleteSelectedItems()
        {
            foreach (NoteItemViewModel item in RichItemsControl.GetItemsControl().Items)
            {
                if (item.IsSelected)
                    RichItemsControl.DeleteItem(item.NoteItem);
            }
        }
        #endregion

        #region General Helpers
        private static NoteItemViewModel GetNoteItemViewModelFrom(FrameworkElement item)
        {
            return item.DataContext as NoteItemViewModel;
        }
        #endregion
    }
}
