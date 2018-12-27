using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Channels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using FastNote.Core;

namespace FastNote
{
    public class InitDragAndDropBehaviorProperty 
        : AttachedBehaviorProperty<InitDragAndDropBehaviorProperty, FrameworkElement>
    {
        protected override AttachedBehavior<FrameworkElement> CreateAttachedBehavior(DependencyObject d)
        {
            return new InitDragAndDropBehavior((FrameworkElement) d);
        }
    } 

    public class InitDragAndDropBehavior : AttachedBehavior<FrameworkElement>
    {
        private Point startPosition;
        private object originalSource;
        private IDragSource DragSource { get; set; }
        private FrameworkElement DraggableTile => DragSource.GetDraggableTile();
        private FrameworkElement Background => DragSource.GetBackground();

        public InitDragAndDropBehavior(FrameworkElement associatedObject) :
            base(associatedObject)
        {
        }

        public override void OnAttached()
        {
            if (AssociatedObject is IDragSource dragSource)
            {
                DragSource = dragSource;

                DragSource.ItemsChanged += () =>
                {
                    foreach (FrameworkElement item in DragSource.GetItems())
                    {
                        item.PreviewMouseLeftButtonDown -= Item_OnPreviewMouseLeftButtonDown;
                        item.PreviewMouseMove -= Item_OnPreviewMouseMove;

                        item.PreviewMouseLeftButtonDown += Item_OnPreviewMouseLeftButtonDown;
                        item.PreviewMouseMove += Item_OnPreviewMouseMove; 
                    }
                };

                Background.PreviewMouseMove += Background_OnPreviewMouseMove;
                Background.MouseLeftButtonUp += Background_OnMouseLeftButtonUp;
            }
            else
                throw new ArgumentException("This behavior could be attached only to objects of type IDragSource");
        }

        public override void OnDetaching()
        {
            if (AssociatedObject is IDragSource)
            {
                DragSource.ItemsChanged += () =>
                {
                    foreach (FrameworkElement item in DragSource.GetItems())
                    {
                        item.PreviewMouseLeftButtonDown -= Item_OnPreviewMouseLeftButtonDown;
                        item.PreviewMouseMove -= Item_OnPreviewMouseMove;
                    }
                };

                Background.PreviewMouseMove -= Background_OnPreviewMouseMove;
                Background.MouseLeftButtonUp -= Background_OnMouseLeftButtonUp;
            }
        }

        private void Item_OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            InitDragAndDrop((FrameworkElement)sender, e);
        }

        public void Background_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            EndDragAndDrop();
        }

        private void Item_OnPreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (e.OriginalSource == originalSource)
            {
                StartDragAndDropIfPossible((FrameworkElement)sender, e);
            }
        }

        public void Background_OnPreviewMouseMove(object sender, MouseEventArgs e)
        {
            UpdateDraggableTilePosition(e);
        }

        public void InitDragAndDrop(FrameworkElement item, MouseButtonEventArgs e)
        {
            startPosition = e.GetPosition((IInputElement)e.OriginalSource);
            originalSource = e.OriginalSource;

            NoteItemViewModel noteItemViewModel = GetNoteItemViewModelFrom(item);
            NoteItem noteItem = noteItemViewModel.NoteItem;

            ViewModelLocator.ApplicationViewModel.DraggingObject = noteItem;
        }

        public void EndDragAndDrop()
        {
            HideDraggableTile();
            ViewModelLocator.ApplicationViewModel.DraggingObject = null;
            ViewModelLocator.ApplicationViewModel.IsDragActive = false;
        }

        public void UpdateDraggableTilePosition(MouseEventArgs e)
        {
            DraggableTile.Margin = new Thickness(
                e.GetPosition(Background).X - DraggableTile.ActualWidth / 4,
                e.GetPosition(Background).Y - DraggableTile.ActualHeight / 2, 0, 0);
        }

        private void ShowDraggableTile()
        {
            DraggableTile.Visibility = Visibility.Visible;
        }

        private void HideDraggableTile()
        {
            DraggableTile.Visibility = Visibility.Hidden;
        }

        public void StartDragAndDropIfPossible(FrameworkElement item, MouseEventArgs e)
        {
            if (ShouldStartDragAndDrop() && DraggedEnoughDistance(e))
            {
                ShowDraggableTile();

                NoteItemViewModel noteItemViewModel = GetNoteItemViewModelFrom(item);
                StartDragAndDrop(noteItemViewModel);
            }
        }

        private bool ShouldStartDragAndDrop()
        {
            return ViewModelLocator.ApplicationViewModel.DraggingObject != null &&
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

        private static bool LeftMouseButtonPressed(MouseEventArgs e)
        {
            return e.LeftButton == MouseButtonState.Pressed;
        }

        private void StartDragAndDrop(NoteItemViewModel noteItemViewModel)
        {
            DraggableTile.Tag = noteItemViewModel.Content;
            noteItemViewModel.IsSelected = false;

            if (!IsControlPressed())
                DragSource.DeleteItem(noteItemViewModel.NoteItem);

            ViewModelLocator.ApplicationViewModel.IsDragActive = true;
        }

        private static NoteItemViewModel GetNoteItemViewModelFrom(FrameworkElement item)
        {
            return item.DataContext as NoteItemViewModel;
        }

        private static bool IsControlPressed()
        {
            return Keyboard.Modifiers.HasFlag(ModifierKeys.Control);
        }
    }
}
