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
    }
}