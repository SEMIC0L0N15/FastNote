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

            if (ViewModelBase.IsInDesignModeStatic)
                this.DataContext = new NoteGroupListDesignModel();
            else
                this.DataContext = ViewModelLocator.NoteGroupListViewModel;
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

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ListBox listBox)
            {
                var selectedGroup = ((NoteGroupViewModel) listBox.SelectedItem).NoteGroup;
                ViewModelLocator.NoteGroupListViewModel.SelectedGroup = selectedGroup;
            }
        }
    }
}