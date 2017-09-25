using System;
using System.Windows;
using FastNote.Core;
using System.Windows.Controls;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;

namespace FastNote
{
    public partial class NoteGroupListControl : UserControl
    {
        private bool isDragOn;
        public NoteGroupListViewModel ViewModel => (NoteGroupListViewModel) DataContext;

        public NoteGroupListControl()
        {
            InitializeComponent();
            DataContext = ViewModelLocator.GetNoteGroupListViewModel();
        }

        private void Button_OnClick(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement contentElement)
            {
                if (contentElement.TemplatedParent is ListBoxItem listBoxItem)
                {
                    listBoxItem.IsSelected = true;
                }
            }
        }

        private void FrameworkElement_OnLoaded(object sender, RoutedEventArgs e)
        {
            ((ListBox) sender).SelectedIndex = 1;
        }

        private void ListBoxItem_OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (ViewModelLocator.ApplicationViewModel.IsDragActive)
            {
                isDragOn = true;
            }
        }

        private void ListBoxItem_OnMouseLeave(object sender, MouseEventArgs e)
        {
            isDragOn = false;
        }

        private void ListBoxItem_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (isDragOn)
            {
                var listBoxItem = (ListBoxItem) sender;
                var noteGroupViewModel = (NoteGroupViewModel) listBoxItem.Content;

                if (ViewModelLocator.ApplicationViewModel.DraggingObject is NoteItem noteItem)
                {
                    noteGroupViewModel.NoteGroup.AddNote(noteItem);
                    isDragOn = false;
                }
            }
        }
    }
}