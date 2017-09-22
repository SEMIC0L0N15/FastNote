using System;
using System.Windows;
using FastNote.Core;
using System.Windows.Controls;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;

namespace FastNote
{
    public partial class NoteGroupListControl : UserControl
    {
        public NoteGroupListControl()
        {
            InitializeComponent();
            this.DataContext = ViewModelLocator.GetNoteGroupListViewModel();
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

        private void ListBox_OnDragEnter(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent("myFormat") ||
                sender == e.Source)
            {
                e.Effects = DragDropEffects.None;
            }
        }

        private void ListBox_OnDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("myFormat"))
            {
                var listBox = sender as ListBox;
                var noteItemViewModel = e.Data.GetData("myFormat") as NoteItemViewModel;

                var noteGroupViewModel = new NoteGroupViewModel(new NoteGroup(noteItemViewModel.Content));

                ((NoteGroupListViewModel)listBox.DataContext).Items.Add(noteGroupViewModel);
            }
        }
    }
}